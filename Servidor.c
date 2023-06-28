#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <mysql.h>
#include <pthread.h>
//#include <my_global.h>



//Declaracion de estructuras
typedef struct{
	char nombre[20];
	int socket;
}Conectado;

typedef struct {
	Conectado conectados[100];
	int num;
}ListaConectados;

typedef struct {
	char nombre1[20];
	char nombre2[20];
	char nombre3[20];
}Partida;

typedef struct {
	Partida partidas[100];
	int num;
}ListaPartidas;

//Declaracion de variables globales
int contador_servicios;
int i;
int sockets[100];
ListaConectados miLista2;
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
char desconectado[20];
ListaPartidas miListaPartidas;

char nombreAnfitrion[20];

//Funciones
int Pon (ListaConectados *lista, char nombre[20], int socket){
	//Anade conectados a la lista
	if(lista ->num ==100)
		return -1;
	else{
		strcpy(lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num ++;
		printf ("Conectado: %s\n", lista->conectados[lista->num -1].nombre);
		return 3;
	}
}
int DameSocket (ListaConectados *lista, char nombre[20]){
	//Devuelve el socket del nombre introducido que esta en la lista
	int i=0;
	int encontrado=0;
	while ((i<lista->num)&&!encontrado){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
			encontrado =1;
		else
			i=i+1;
	}
	if (encontrado)
		return lista->conectados[i].socket;
	else
		return -1;
}
int DamePosicion (ListaConectados *lista, char nombre[20]){
	//Devuelve la posicion del nombre introducido que esta en la lista
	int i=0;
	int encontrado=0;
	while ((i<lista->num)&&!encontrado){
		if (strcmp(lista->conectados[i].nombre,nombre)==0)
		{
			encontrado = 1;
		}
		else
			i=i+1;
	}
	if (encontrado)
		return i;
	else
		return -1;
}

void PartidaNueva(char* nombre1, char* nombre2, char* nombre3, ListaPartidas* lista) {
	// Crear una nueva partida
	Partida nuevaPartida;
	strcpy(nuevaPartida.nombre1, nombre1);
	strcpy(nuevaPartida.nombre2, nombre2);
	strcpy(nuevaPartida.nombre3, nombre3);
	
	// Agregar la nueva partida a la lista de partidas
	lista->partidas[lista->num] = nuevaPartida;
	lista->num++;
	if (strcmp (nombre3, "")==0)
		printf("Nueva partida %d, Jugadores: %s, %s\n", lista->num, nombre1, nombre2 );
	else
		printf("Nueva partida %d, Jugadores: %s, %s, %s\n", lista->num, nombre1, nombre2, nombre3 );
}

int Elimina (ListaConectados *lista, char nombre[20]){
	//Elimina en nombre de la lista de conectados
	int pos = DamePosicion (lista, nombre);
	if (pos==-1)
		return -1;
/*	else {*/
/*		int i;*/
/*		for (i=pos; i<lista->num-1; i++)*/
/*		{*/
/*			lista-> conectados [i] = lista->conectados[i+1];*/
			//strcpy(lista->conectados[i].nombre = lista->conectados[i+1].nombre, nombre);
			//lista->conectados[i].socket =lista->conectados[i+1].socket;
/*		}*/
	else {
		// Guardar el nombre en desconectado antes de que se elimine
		strcpy(desconectado, lista->conectados[pos].nombre);
		
		for (int i = pos; i < lista->num-1; i++) {
			lista->conectados[i] = lista->conectados[i+1];
		}
		lista->num--;
		return 0;
	}
}

void EliminarPartida (ListaPartidas* lista, int numpartida){
	int numPartidas = lista->num;
	
	if (numpartida < 0 || numpartida >= numPartidas) {
		
	}
	else{
		for (int i = numpartida; i < numPartidas - 1; i++) {
		memcpy(&lista->partidas[i], &lista->partidas[i + 1], sizeof(Partida));
		}
		lista->num--;
		
	}
}

void DameConectados (ListaConectados *lista, char conectados[300]){
	//Pone en un vector los nombres de conectados separados por una bcoma
	//Primero pone el n￯﾿ﾽmero de conectados. Ej: 3,Maria,Juan,Pedro
	sprintf (conectados, "%d", lista->num);
	printf ("%d",lista->num);
	int i;
	for (i=0; i<lista->num; i++)
		sprintf (conectados, "%s,%s\n", conectados, lista->conectados[i].nombre);
}

void DameTodosSockets (ListaConectados *lista, char conectados[300], char sockets[300]){
	//Pone en un vector los sockets de conectados separados por una coma. Ej: 2,3,4
	int i;
	int o =0;
	char socket[10];
	char nombre[20];
	char *p = strtok (conectados, ",");
	int n = atoi (p);
	p = strtok (NULL, ",");
	strcpy (nombre, p);
	
	for (;;)
		if (strcmp (lista->conectados[i].nombre, nombre)==0){
			sprintf(socket, "%d", lista->conectados[i].socket);
			strcat (sockets, socket); 
			if (o< n-1){
				strcat (sockets, ",");
				p = strtok (NULL, ",");
				strcpy (nombre, p);
			}
			o++;
	}
}


void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s = (int * ) socket;
	sock_conn = *s;
	int ret;
	MYSQL *conn;
	int err;
	
	//Establecer conexion con la base de datos
	conn = mysql_init(NULL);
	if (conn==NULL) 
	{
		printf ("Error al crear la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//Inicializa la conexion
	conn = mysql_real_connect (conn, "Localhost","root", "mysql", "M9_JUEGO",0, NULL, 0);
	//conn = mysql_real_connect (conn, "shiva2.upc.es","root", "mysql",NULL, 0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexion: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	char peticion[512];
	char respuesta[512];
	int terminar =0;
	
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		char respuesta[512];
		MYSQL_RES *resultado;
		MYSQL_ROW row;
		// Ahora recibimos la peticion
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		// Tenemos que anadirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		printf ("P: %s\n",peticion);
		
		//Atendemos la peticion
		char *t = strtok (peticion, "/");
		int codigo =  atoi (t);
		printf ("Codigo: %d \n", codigo);
		char nombre[20];
		
		if (codigo ==0) //peticion de desconexion
		{
			t = strtok( NULL, "/");
			char nombre_usuario[40];
			
			if (t!= NULL)
			{
				strcpy (nombre_usuario, t);
				pthread_mutex_lock(&mutex); //No interrumpir
				int eliminar = Elimina (&miLista2, nombre_usuario);
				pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
				if (eliminar==0)
					printf("Usuario eliminado de la lista de conectados. \n");
				else
					printf("Error al eliminar el usuario de la lista de conectados. \n");
			}
			terminar=1;
		}
		
		else if (codigo ==100)
		{
			//ID/Contrase￯﾿ﾽa
			char nombre_usuario[40];
			char contrasena [40];
			char consulta [800];
			
			t = strtok( NULL, "/");
			strcpy (nombre_usuario, t);
			printf("Nombre usuario %s\n", nombre_usuario);
			
			strcpy (consulta,"SELECT JUGADORES.Contrasena FROM JUGADORES WHERE JUGADORES.USUARIO = '"); 
			strcat (consulta, nombre_usuario);
			strcat (consulta,"'");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos2 de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido usuario en la consulta\n");
				sprintf (respuesta,"100/NoUser");
			}
			
			else
			{
				t = strtok( NULL, "/");
				strcpy (contrasena, t);
				printf("Con recib %s\n", contrasena);
				printf("con db %s\n",row[0]);
				
				if (strcmp(contrasena,row[0]) == 0)
				{
					sprintf (respuesta,"100/Correct");
					pthread_mutex_lock(&mutex); //No interrumpir
					int poner = Pon (&miLista2, nombre_usuario, sock_conn);
					//int poner = Pon (&miLista2, nombre_usuario, miLista2.num);
					pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
					if (poner == 3)
					{
						printf("Usuario anadido a la lista de conectados. \n");
						
					}
					else
						printf("Error al introducir al usuario a la lista de conectados. \n");
				}
				
				else 
				{
					sprintf (respuesta,"100/Incorrect");
				}	
			}	
			printf ("%s",respuesta);
		}
		else if( codigo == 720)
		{
			char usuario[20];
			char contractual[50];
			char contranueva[50];
			char consulta[200];
			strcpy(respuesta, "720/");
			
			t = strtok(NULL, "/");
			strcpy(usuario, t);
			
			t = strtok(NULL, "/");
			strcpy(contractual, t);
			
			t = strtok(NULL, "/");
			strcpy(contranueva, t);
			
			
			
			strcpy(consulta, "UPDATE JUGADORES SET Contrasena = '");
			strcat(consulta, contranueva);
			strcat(consulta, "' WHERE USUARIO = '");
			strcat(consulta, usuario);
			strcat(consulta, " ' AND Contrasena = '");
			strcat(consulta, contractual);
			strcat(consulta, "'");
			
			
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos2 de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				
			}
			else
			{
				//sprintf(respuesta, "888/Correct");
			}
			
			int numRowsAffected = mysql_affected_rows(conn);
			
			if(numRowsAffected > 0)
			{
				sprintf(respuesta, "720/Correct");
			}
			else
			{
				sprintf(respuesta, "720/Incorrect");
			}
		}
			
		
		else if (codigo ==101)
		{
			//ID/Contrase￯﾿ﾽa
			char nombre_usuario[40];
			char contrasena [40];
			char consulta [800];
			
			t = strtok( NULL, "/");
			strcpy (nombre_usuario, t);
			printf("Nombre usuario %s\n", nombre_usuario);
			
			t = strtok( NULL, "/");
			strcpy (contrasena, t);
			printf("Contrasena %s\n", contrasena);
			
			strcpy (consulta,"SELECT JUGADORES.ID FROM JUGADORES WHERE JUGADORES.USUARIO ='"); 
			strcat (consulta,nombre_usuario);
			strcat (consulta,"';");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos2 de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			
			resultado = mysql_store_result (conn); 
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				
				strcpy (consulta,"SELECT COUNT(ID) FROM JUGADORES;"); 
				
				err=mysql_query (conn, consulta);
				if (err!=0) {
					printf ("Error al consultar datos2 de la base %u %s\n",
							mysql_errno(conn), mysql_error(conn));
					exit (1);
				}
				
				int max_ID;
				char ID[80];
				
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				
				max_ID = atoi(row[0]) + 1;
				
				strcpy (consulta, "INSERT INTO JUGADORES VALUES (");
				//convertimos el ID en un string y lo concatenamos 
				
				sprintf(ID, "%d", max_ID);
				printf("ID: %s\n",ID);
				strcat (consulta, ID); 
				strcat (consulta, ",'");
				//concatenamos el nombre
				strcat (consulta, nombre_usuario); 
				strcat (consulta, "','");
				//concatenamos la contrasena
				strcat (consulta, contrasena); 
				strcat (consulta, "');");
				
				err = mysql_query(conn, consulta);
				if (err!=0) {
					printf ("Error al introducir datos la base %u %s\n", 
							mysql_errno(conn), mysql_error(conn));
					sprintf (respuesta, "101/Incorrect2");
				}
				
				else
				{
					sprintf (respuesta,"101/Correct");
				}	
				printf ("%s",respuesta);
			}
			else
				sprintf (respuesta, "101/Incorrect");
		}
		else if (codigo ==888)
		{
			//ID/Contrase￯﾿ﾽa
			char nombre_usuario[40];
			char contrasena [40];
			char consulta [800];
			
			t = strtok( NULL, "/");
			strcpy (nombre_usuario, t);
			printf("Nombre usuario %s\n", nombre_usuario);
			
			t = strtok( NULL, "/");
			strcpy (contrasena, t);
			printf("Contrasena %s\n", contrasena);
			
			strcpy(consulta, "DELETE FROM JUGADORES WHERE JUGADORES.USUARIO='");
			strcat(consulta, nombre_usuario);
			strcat(consulta, "' AND JUGADORES.Contrasena = '");
			strcat(consulta, contrasena);
			strcat(consulta, "';");
			
			err=mysql_query (conn, consulta);
			if (err!=0) {
				printf ("Error al consultar datos2 de la base %u %s\n",
						mysql_errno(conn), mysql_error(conn));
				
				
			}
			else
			{
				//sprintf(respuesta, "888/Correct");
			}
			
			int numRowsAffected = mysql_affected_rows(conn);
			
			if(numRowsAffected > 0)
			{
				sprintf(respuesta, "888/Correct");
			}
			else
			{
				sprintf(respuesta, "888/Incorrect");
			}
		}
		
		else if (codigo ==1) //Consultar el numero de partidas que ha ganado un jugador
		{	
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			//nombre
			char nombre[40];
			
			t = strtok( NULL, "/");
			strcpy (nombre, t);
			printf("Nombre usuario %s\n", nombre);
			
			strcpy (consulta,"SELECT COUNT(*) FROM PARTIDAS WHERE GANADOR='"); 
			strcat (consulta, nombre);
			strcat (consulta,"'");
			
			
			/*strcpy (consulta,"SELECT COUNT(PARTIDAS.ID) FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE PARTIDAS.GANADOR= '"); 
			strcat (consulta, nombre);
			strcat (consulta,"' AND JUGADORES.ID=PARTICIPACION.ID_J AND PARTIDAS.ID=PARTICIPACION.ID_P     AND    PARTICIPACION.ID_J = (SELECT JUGADORES.ID FROM JUGADORES WHERE JUGADORES.USUARIO = '");
			strcat (consulta, nombre);
			strcat (consulta, "');");*/
			
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			
			else 
			{
				printf ("El jugador %s ha ganado %s s\n", nombre, row[0]);
			}
			
			sprintf (respuesta,"1/%s",row[0]);
		}
		
		else if (codigo == 2)//Consultar jugadores que han ganado una partida de mas de 10 minutos
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			
			strcpy (consulta,"SELECT DISTINCT PARTIDAS.GANADOR FROM (PARTIDAS, JUGADORES, PARTICIPACION)  WHERE  PARTIDAS.DURACION > 10   AND    JUGADORES.ID = PARTICIPACION.ID_J   AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 
			
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			char vector_nombres[500];
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			
			else 
			{
				strcpy(vector_nombres,row[0]);
				row = mysql_fetch_row (resultado);
				while (row !=NULL) 
				{
					strcat(vector_nombres," ");
					strcat(vector_nombres,row[0]);
					row = mysql_fetch_row (resultado);
				}
				sprintf (respuesta,"2/%s", vector_nombres);
			}
			
		}
		
		else if (codigo == 3) //Consultar fecha y horade una patida
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			char IDpartida [10];
			
			t = strtok( NULL, "/");
			strcpy (IDpartida, t);
			strcpy (consulta,"SELECT FECHA_HORA FROM PARTIDAS WHERE ID = ");
			strcat (consulta,IDpartida);
			//strcat (consulta," AND    JUGADORES.ID = PARTICIPACION.ID_J		AND    PARTIDAS.ID = PARTICIPACION.ID_P;"); 
			
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			
			else 
			{
				if (row[0] == 0)
					strcpy(respuesta,"3/NoExist");
				else
					sprintf (respuesta,"3/%s", row[0]);
			}
			
		}
		
		
		
		else if (codigo == 4)
		{
			MYSQL_RES *resultado;
			MYSQL_ROW row;
			char consulta [800];
			char dia [20];
			char usuario [20];
			
			t = strtok( NULL, "-");
			strcpy (usuario, t);
			
			printf("Usuario=%s=", usuario);
			
			t = strtok( NULL, "-");
			strcpy (dia, t);
			
			printf("Dia=%s=", dia);
			strcpy (consulta,"SELECT COUNT(*) FROM PARTIDAS WHERE FECHA_HORA LIKE '");
			strcat (consulta,dia);
			strcat (consulta,"/%' AND GANADOR = '");
			strcat (consulta,usuario);
			strcat (consulta, "'");
			
			/*strcpy (dia, t);
			strcpy (consulta,"SELECT COUNT(PARTICIPACION.ID_J) FROM (PARTIDAS, JUGADORES, PARTICIPACION) WHERE  SUBSTRING(PARTIDAS.FECHA_HORA, 1, 10)  = '");
			strcat (consulta,dia);
			strcat (consulta,"' AND PARTICIPACION.ID_J = (SELECT ID FROM JUGADORES WHERE USUARIO = '");
			strcat (consulta,usuario);
			strcat (consulta, "')	AND    JUGADORES.ID = PARTICIPACION.ID_J	AND    PARTIDAS.ID = PARTICIPACION.ID_P;");*/
			
			err=mysql_query (conn, consulta);
			
			if (err!=0) {
				printf ("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				exit (1);
			}
			//Recogemos el resultado de la consulta
			resultado = mysql_store_result (conn);
			row = mysql_fetch_row (resultado);
			
			if (row == NULL)
			{
				printf ("No se han obtenido datos en la consulta\n");
			}
			
			else 
			{
				char resp[20];
				sprintf(resp,"4/%d",atoi(row[0]));
				strcpy(respuesta, resp);
			}	
			
		}
		else if(codigo == 990) // Enviamos codigo invitacion solo a los invitados.
		{
			
			char nombre[20]; // nombre del invitado
			strcpy(respuesta, "990/"); // 990
			
			t = strtok( NULL, "/");
			strcat(respuesta, t); // invitacion
			strcat(respuesta, "/");
			t = strtok( NULL, "/"); 
			strcat(respuesta, t); // nombre anfitrion
			strcat(respuesta, "/");
			
			strcpy(nombreAnfitrion, t); // guardamos el nombre del anfitrion... solo nos permite tener un anfitrion/servidor.
			
			t = strtok( NULL, "/");
			strcat(respuesta, t); // nombre invitado
			strcpy(nombre, t);
			printf("Enviando invitacion respuesta %s.\n", respuesta);
			
			pthread_mutex_lock(&mutex); //No interrumpir
			
			for(int j = 0; j < miLista2.num; j++)
			{
				printf("Nombre lista |%s| posicion %i nombre cmp |%s|", miLista2.conectados[j].nombre, j, nombre);
				if(strcmp(miLista2.conectados[j].nombre, nombre) == 0)
				{
					//write (miLista2.conectados[j].socket, respuesta, strlen(respuesta));
					printf("Enviando invitacion a %s", nombre);
				}
				
				write (sockets[j], respuesta, strlen(respuesta));
			}
			
			pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
		}
		else if(codigo == 991) // Enviamos codigo aceptacion/rechazo invitacion.
		{
			
			char nombre[20]; // nombre del invitado
			strcpy(respuesta, "991/"); // 991
			
			t = strtok( NULL, "/");
			strcat(respuesta, t); // invitacion
			strcat(respuesta, "/");
			t = strtok( NULL, "/"); 
			strcat(respuesta, t); // nombre anfitrion
			strcat(respuesta, "/");
			t = strtok( NULL, "/");
			strcat(respuesta, t); // nombre invitado
			strcat(respuesta, "/");
			t = strtok( NULL, "/");
			strcat(respuesta, t); // acepta/rechaza
			
			printf("Enviando respuesta a invitacion %s. \n", respuesta);
			
			pthread_mutex_lock(&mutex); //No interrumpir
			
			for(int j = 0; j < miLista2.num; j++)
			{
				printf("Nombre lista |%s| posicion %i nombre cmp |%s| \n", miLista2.conectados[j].nombre, j, nombreAnfitrion);
				write (sockets[j], respuesta, strlen(respuesta));
			}
			
			pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
		}

		else if(codigo == 997)		 
		{
			char mensaje[100];
			
			char username[20]; // nombre del que envia el mensaje
			
			strcpy(respuesta, "997/"); // 997
			t = strtok( NULL, "/"); //texto mensaje
			
			strcpy(mensaje, t); //
			
			t = strtok( NULL, "/");
			
			strcpy(username, t); // username
			pthread_mutex_lock(&mutex); //No interrumpir
			sprintf(respuesta, "997/%s/%s/%s", mensaje, username, nombreAnfitrion);
			printf("Enviando mensaje %s. \n", respuesta);
			
			for(int j = 0; j < miLista2.num; j++)
			{
				printf("Enviando mensaje de chat a %s: %s. \n",miLista2.conectados[j].nombre, respuesta);
				write (sockets[j], respuesta, strlen(respuesta));
			}
			pthread_mutex_unlock(&mutex); //No interrumpir
		}
		
		else if(codigo == 999) // empieza la partida
		{
			char participantes[100];
			char nombre1[20];
			char nombre2[20];
			char nombre3[20];
			char username[20]; // nombre del que envia el mensaje
			
			strcpy(respuesta, "999/"); // 999
			
			t = strtok( NULL, "/"); //texto mensaje
			
			strcpy(username, t); //
			
			t = strtok( NULL, "/");
			
			strcpy(participantes, t); // username
			pthread_mutex_lock(&mutex); //No interrumpir
			
			char* name = strtok(participantes, "-");
			strcpy(nombre1, name);
			
			name = strtok(NULL, "-");
			strcpy(nombre2, name);
			name = strtok(NULL, "-");
			if(name == NULL)
			{
				strcpy(nombre3, "");
			}
			else{
				strcpy(nombre3, name);
			}
			//A￱adirla a ListaPartidas
			PartidaNueva(nombre1, nombre2, nombre3, &miListaPartidas);
			int numero_partida = miListaPartidas.num;
			if (strcmp(nombre3, "")==0)
			{
				sprintf(respuesta, "999/%d/%s/%s-%s", numero_partida, username, nombre1, nombre2);
			}
			else
				sprintf(respuesta, "999/%d/%s/%s-%s-%s", numero_partida, username, nombre1, nombre2, nombre3);
			printf("Enviando mensaje %s. \n", respuesta);
			
			for(int j = 0; j < miLista2.num; j++)
			{
				printf("Enviando mensaje de chat a %s: %s. \n",miLista2.conectados[j].nombre, respuesta);
				write (sockets[j], respuesta, strlen(respuesta));
			}
			
			pthread_mutex_unlock(&mutex); //No interrumpir
		}
		
		else if (codigo == 50)//Tanque
		{
			int numpartida;
			char jugador[5];
			int x;
			int y;
			int direccion;
			
			strcpy(respuesta, "50/"); // 50
			
			t = strtok( NULL, "/"); //numpartida
			numpartida = atoi(t);
			
			t = strtok( NULL, "/");
			strcpy(jugador, t);
			
			t = strtok( NULL, "/");
			x = atoi(t);
			t = strtok( NULL, "/");
			y = atoi(t);
			t = strtok( NULL, "/");
			direccion = atoi(t);
			
			pthread_mutex_lock(&mutex);
			
			sprintf(respuesta, "50/%d/%s/%d/%d/%d/", numpartida, jugador, x, y, direccion);
			for(int j = 0; j < miLista2.num; j++)
			{
				write (sockets[j], respuesta, strlen(respuesta));
				
			}
			
			pthread_mutex_unlock(&mutex);
		}
		
		else if (codigo == 51)//Balas
		{
			int numpartida;
			char oponente[5];
			int x;
			int y;
			char direccion[20];
			
			strcpy(respuesta, "51/"); // 52
			
			t = strtok( NULL, "/"); //numpartida
			numpartida = atoi(t);
			t = strtok( NULL, "/");
			strcpy(oponente, t);
			t = strtok( NULL, "/");
			x = atoi(t);
			t = strtok( NULL, "/");
			y = atoi(t);
			t=strtok(NULL,"/");
			strcpy(direccion, t);
			
			pthread_mutex_lock(&mutex);
			
			sprintf(respuesta, "51/%d/%s/%d/%d/%s/", numpartida, oponente, x, y, direccion);
			for(int j = 0; j < miLista2.num; j++)
			{
				write (sockets[j], respuesta, strlen(respuesta));
				
			}
			
			pthread_mutex_unlock(&mutex);
		}
		
		else if (codigo == 52)//Cubo
		{
			int numpartida;
			int num_cubo;
			int num_img;
			
			strcpy(respuesta, "52/"); // 52
			
			t = strtok( NULL, "/"); //numpartida
			numpartida = atoi(t);
			
			t = strtok( NULL, "/");
			num_cubo = atoi(t);
			t = strtok( NULL, "/");
			num_img = atoi(t);
			
			pthread_mutex_lock(&mutex);
			
			sprintf(respuesta, "52/%d/%d/%d/", numpartida, num_cubo, num_img);
			for(int j = 0; j < miLista2.num; j++)
			{
				write (sockets[j], respuesta, strlen(respuesta));
				
			}
			
			pthread_mutex_unlock(&mutex);
		}
		
		
		else if (codigo == 53)//PowerUp
		{
			int numpartida;
			int num_cubo;
			int vidas;
			char oponente[5];
			
			strcpy(respuesta, "53/"); // 53
			
			t = strtok( NULL, "/"); //numpartida
			numpartida = atoi(t);
			
			t = strtok( NULL, "/");
			num_cubo = atoi(t);
			t = strtok( NULL, "/");
			vidas = atoi(t);
			t=strtok(NULL,"/");
			strcpy(oponente, t);
			pthread_mutex_lock(&mutex);
			
			sprintf(respuesta, "53/%d/%d/%d/%s/", numpartida, num_cubo, vidas, oponente);
			for(int j = 0; j < miLista2.num; j++)
			{
				write (sockets[j], respuesta, strlen(respuesta));
				
			}
			
			pthread_mutex_unlock(&mutex);
		}
		
		
		else if (codigo == 54)//Vidas
		{
			int numpartida;
			char oponente[5];
			int vidas;
			
			strcpy(respuesta, "54/"); // 54
			
			t = strtok( NULL, "/"); //numpartida
			numpartida = atoi(t);
			
			t = strtok( NULL, "/");
			strcpy(oponente, t);
			t = strtok( NULL, "/");
			vidas = atoi(t);
			
			pthread_mutex_lock(&mutex);
			
			sprintf(respuesta, "54/%d/%s/%d/", numpartida, oponente, vidas);
			for(int j = 0; j < miLista2.num; j++)
			{
				write (sockets[j], respuesta, strlen(respuesta));
				
			}
			
			pthread_mutex_unlock(&mutex);
		}
		
		if (codigo != 0)
		{
			// Enviamos respuesta
			printf("\nrespuesta: %s\n",respuesta);
			
			write (sock_conn, respuesta, strlen(respuesta));
		}
		
		if ((codigo ==1)||(codigo ==2)||(codigo ==3)||(codigo ==4))//quitar codigo enviar confirmacion invitacion
		{
			pthread_mutex_lock(&mutex); //No interrumpir
			contador_servicios = contador_servicios +1;
			pthread_mutex_unlock(&mutex); //Ahora si se puede interrumpir
			//notificar
			char notificacion[20];
			sprintf (notificacion,"6/%d",contador_servicios);
			int j;
			for (j=0; j<i; j++)
				write (sockets[j], notificacion, strlen(notificacion));
			
		}
		if (codigo ==100 /*|| codigo==0*/)
		{
			char misConectados [300];
			char notificacion [310];
			DameConectados (&miLista2, misConectados);
			sprintf(notificacion, "7/%s",misConectados);
			int j;
			for (j=0; j<i; j++)
				
				write (sockets[j], notificacion, strlen(notificacion));
		}
		if (codigo==0)
		{
			//char misConectados [300];
			char notificacion [310];
			//DameConectados (&miLista2, misConectados);
			//sprintf(notificacion, "7/%i,%s", miLista2.num, desconectado);
			strcat(desconectado, "\n");
/*			sprintf(notificacion, "7/%s", desconectado);*/
			sprintf(notificacion, "7/%i,%s", miLista2.num, desconectado);
			int j;
			for (j=0; j<i; j++)
				
				write (sockets[j], notificacion, strlen(notificacion));
		}
	}
	//Finalizamos el servicio al cliente
	close(sock_conn);
	mysql_close (conn);
}	

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	
	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// Establecemos el puerto 
	serv_adr.sin_port = htons(9500);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	pthread_t thread[100];
	i=0;
	
	//Inicializacion de variables globales
	contador_servicios = 0;
	miLista2.num = 0;
	
	// Bucle infinito
	for (;;){
		printf ("Escuchando...\n");
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion.\n");
		//sock_conn es el socket que usaremos para este cliente
		sockets[i] = sock_conn;
		pthread_create (&thread[i], NULL, AtenderCliente, &sockets[i]);
		i++;
	}
}
