#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdint.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#include <pthread.h>

#include <errno.h> 
#include <wiringPi.h>
#include <wiringSerial.h>
#include <wiringPiSPI.h>

#define BUFF_SIZE 1024
#define RAIL_PIN 6
const int CaptureFail = -1;
const int spi_channel = 0;
const int spi_speed = 1000000;
const int adc_channel1 = 0;
const int adc_channel2 = 2;
const int adc_channel3 = 4;

int cnt = 0;
int passCnt = 0;
int failCnt = 0;
char * device[4];


int fd = -1;
int fd2 = -1;
unsigned long baud = 9600;
int MotorFlag = 0;
int StartFlag = 0;

int ImageSideCnt = 0;
int ArmSideCnt = 0;
int MotorSideCnt = 0;
int NothingCnt = 0;

void SendCommand(char * command);
void MoveRobotArm(int command);
void setup(void);

pthread_t motorThread;
pthread_t startCmdThread;

typedef struct Args{
	int serverSocket;
	int clientSocket;
	struct sockaddr_in clientaddr;
}Args;



typedef struct Node{
	int data;
	struct Node *next;
}Node;

typedef struct Queue{
	Node *front;
	Node *rear;
	int count;
}Queue;

void initQueue(Queue *queue);
int isEmpty(Queue *queue);
void Enqueue(Queue *queue, int data);
int Dequeue(Queue *queue);



void * MotorMove(void * arg);
void * ListenStartCmd(void * arg);



int CheckPhotoSensorValue();

int ServerOpen(struct sockaddr_in * server_addr, char * port);

int PCBCapture();

int Listen_Client(int server_socket, struct sockaddr_in * client_addr);

int SendFile(int client_socket);

int ReceiveCommand(int client_socket);



enum {Create_Fail = -1, Bind_Fail = -2, Listening_Fail = -3, Acception_Fail = -4};

int main(int argc, char** argv)
{
	struct sockaddr_in* server_addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
	struct sockaddr_in* client_addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
	struct sockaddr_in* cmd_server_addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
	struct sockaddr_in* cmd_client_addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
	struct sockaddr_in* work_server_addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
	struct sockaddr_in* work_client_addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
	
	
	int* server_socket = (int *)malloc(sizeof(int));
	int* client_socket = (int *)malloc(sizeof(int));
	int* cmd_server_socket = (int *)malloc(sizeof(int));
	int* cmd_client_socket = (int *)malloc(sizeof(int));
	int* work_server_socket = (int *)malloc(sizeof(int));
	int* work_client_socket = (int *)malloc(sizeof(int));
	
	char cmd_port[20];
	char work_port[20];
	
	for(int i=0;i<4;i++)
	{
		device[i] = (char *)malloc(sizeof(char) * 20);
	}
	strcpy(device[0], "/dev/ttyUSB0");
	strcpy(device[1], "/dev/ttyUSB1");
	strcpy(device[2], "/dev/ttyACM0");
	strcpy(device[3], "/dev/ttyACM1");
	
	sprintf(cmd_port,"%d",atoi(argv[1]) + 1);
	sprintf(work_port, "%d", atoi(cmd_port) + 1);
	
	
	wiringPiSetup();
	if(wiringPiSPISetup(spi_channel, spi_speed) == -1)
	{
		printf("SPI Setup Fail!");
		exit(1);
	}
	
	pinMode(RAIL_PIN, OUTPUT);
	
	
	switch(*server_socket = ServerOpen(server_addr, argv[1]))
	{
		case Create_Fail:
			printf("Image Socket Creation Fail\n");
			exit(1);
			
		case Bind_Fail:
			printf("Image Binding Fail\n");
			exit(1);
	}
	
	switch(*cmd_server_socket = ServerOpen(cmd_server_addr, cmd_port))
	{
		case Create_Fail:
			printf("Cmd Socket Creation Fail\n");
			exit(1);
			
		case Bind_Fail:
			printf("Cmd Binding Fail\n");
			exit(1);
	}
	
	switch(*work_server_socket = ServerOpen(work_server_addr, work_port))
	{
		case Create_Fail:
			printf("work Socket Creation Fail\n");
			exit(1);
			
		case Bind_Fail:
			printf("work Binding Fail\n");
			exit(1);
	}
	
	//Robot Arm Arduino Serial port Setup
	setup();
	digitalWrite(RAIL_PIN, 0);

	int adc_value;
	Queue * PFQueue;
	PFQueue = (Queue *)malloc(sizeof(Queue));
	initQueue(PFQueue);
	
	
	Args arg;
	arg.clientSocket = *work_client_socket;
	arg.clientaddr = *work_client_addr;
	arg.serverSocket = *work_server_socket;
	pthread_create(&startCmdThread, NULL, ListenStartCmd,(void *)&arg);

	while(1)
	{			
		digitalWrite(RAIL_PIN, 1);
		while(StartFlag)
		{
			digitalWrite(RAIL_PIN, 0);
			//check All Photo Sensor value every 0.1 sec
			//check Function returns the number of Photo Sensor
			// 1 -- Image
			// 2 -- Robot Arm
			// 3 -- Magazine slot
			// 0 -- No Interrupt
			delay(60);
			adc_value = CheckPhotoSensorValue();
			
			if(adc_value != 0)
			{
				//Stop the Rail
				digitalWrite(RAIL_PIN, 1);
				
				// Image Side Sensor 
				if(adc_value == 1 && NothingCnt > 8)
				{
					printf("Image Start!\n");
					NothingCnt = 0;
					//Capture and Send Rework RAIL
					if(PCBCapture() == CaptureFail)
					{
						printf("CaptureFailed\n");
						exit(1);
					}	
					printf("Captured\n");
					
					
					printf("IP : %s\n", inet_ntoa(server_addr->sin_addr));
					
					*client_socket = Listen_Client(*server_socket, client_addr);
				
					
					printf("client connected\n");
					
					
					if(SendFile(*client_socket) == -1)
					{
						printf("FileSize Sync Fail\n");
						exit(1);
					}
					
					
					close(*client_socket);
					printf("%d sent\n", cnt++);
					
					*cmd_client_socket = Listen_Client(*cmd_server_socket, cmd_client_addr);
					printf("cmd client connected\n");
					int command;
					
					command = ReceiveCommand(*cmd_client_socket);
					if(command == 0)
					{
						printf("Pass Fail Receive Error");
						exit(1);
					}
					printf("received command : %d\n", command);
					Enqueue(PFQueue, command);
				}
				//Robot Arm Side Sensor
				else if(adc_value == 2)
				{
					//Check que of PassORFail and Send Msg to Arduino
					//if Arduino lift up the PCB, send to raspberry "Finish" msg
					int command = Dequeue(PFQueue);
					printf("command : %d\n", command);
					MoveRobotArm(command);
					delay(5000);
				}
				//Magazine Side Sensor
				else if(adc_value == 3)
				{
					//Make Motor Move
					if(MotorFlag == 0)
					{
						pthread_create(&motorThread, NULL, MotorMove,NULL);
						fprintf (stdout, "Unable to start wiringPi: %s\n", strerror (errno)) ;
					}
				}
				else
				{
					//continue
				}
				
				
				//ReStart Rail
				digitalWrite(RAIL_PIN, 0);
			}
				
				
				//printf("OUT : %d\n", adc_value);
				////close(*server_socket);
		}
	}
	
	
	return 0;
}


void * MotorMove(void * arg)
{
	MotorFlag = 1;
	char command[100] = "sudo python3 motor.py";
	system(command);
	delay(9000);
	MotorFlag = 0;
	pthread_exit(NULL);
}

void * ListenStartCmd(void *arg)
{
	Args cmdArg = *(Args *)arg;
	int clientSocket = cmdArg.clientSocket;
	int serverSocket = cmdArg.serverSocket;
	struct sockaddr_in clientaddr = cmdArg.clientaddr;
	
	char command[6];
	while(1)
	{
		memset(command, 0, sizeof(command));
		printf("Listening\n");
		clientSocket = Listen_Client(serverSocket, &clientaddr);
		recv(clientSocket, command, sizeof(command), 0);
		printf("receieved thread : %s\n", command);
		
		if(strcmp(command, "start") == 0)
			StartFlag = 1;
		else if(strcmp(command, "end") == 0)
		{
			char sendPass[BUFF_SIZE];
			char sendFail[BUFF_SIZE];
			sprintf(sendPass, "%d", passCnt);
			send(clientSocket, &passCnt, sizeof(int), 0);
			sprintf(sendFail, "%d", failCnt);
			send(clientSocket, &failCnt, sizeof(int), 0);
			StartFlag = 0;
			passCnt = 0;
			failCnt = 0;
			close(fd);
			fd = -1;
			close(fd2);
			fd2 = -1;
			setup();
		}
	}
}


int CheckPhotoSensorValue()
{
	unsigned char buffer[3];
	
	buffer[0] = 1;
	buffer[1] = (8 + adc_channel1) << 4;
	buffer[2] = 0;
	
	wiringPiSPIDataRW(spi_channel, buffer, 3);
	
	int ImageSideVal = ((buffer[1] & 3) << 8)+buffer[2];
	
	buffer[0] = 1;
	buffer[1] = (8 + adc_channel2) << 4;
	buffer[2] = 0;
	
	wiringPiSPIDataRW(spi_channel, buffer, 3);
	int ArmSideVal = ((buffer[1] & 3) << 8)+buffer[2];
	
	buffer[0] = 1;
	buffer[1] = (8 + adc_channel3) << 4;
	buffer[2] = 0;
	
	wiringPiSPIDataRW(spi_channel, buffer, 3);
	int MotorSideVal = ((buffer[1] & 3) << 8)+buffer[2];
	
	//printf("Image Side : %d    ArmSide :  %d      MotorSide : %d\n", ImageSideVal, ArmSideVal, MotorSideVal);
	if(ImageSideVal > 470)
		ImageSideCnt++;
	else
		ImageSideCnt = 0;
		
	if(ArmSideVal > 600)
		ArmSideCnt++;	
	else
		ArmSideCnt = 0;
		
	if(MotorSideVal >580)
		MotorSideCnt++;
	else
		MotorSideCnt = 0;
	
		
	if(MotorSideCnt > 10 && MotorFlag == 0)
		return 3;
	else if(ArmSideCnt > 10)
		return 2;
	else if(ImageSideCnt > 4)
		return 1;
	
	NothingCnt++;
	return 0;
}

int PCBCapture()	
{
	char command[100] = "sudo python3 /home/pi/Python-3.7.0/camNled.py ";
	const char filename[30] = "sample1.jpg";
	strcat(command,filename);
	int input;
	
	return system(command);
}

int ServerOpen(struct sockaddr_in* server_addr, char * port)
{
	int server_socket;
	if((server_socket = socket(PF_INET, SOCK_STREAM, 0)) < 0)
	{
		return Create_Fail;
	}
	
	memset(server_addr, 0, sizeof(*server_addr));	
	
	server_addr->sin_family = AF_INET;
	(server_addr->sin_addr).s_addr = htonl(INADDR_ANY);
	server_addr->sin_port = htons(atoi(port));
	
	printf("IP : %s\n", inet_ntoa(server_addr->sin_addr));
	
	if(bind(server_socket, (struct sockaddr*)server_addr, sizeof(*server_addr)) < 0)
	{
		return Bind_Fail;
	}
	
	return server_socket;
}

int Listen_Client(int server_socket, struct sockaddr_in* client_addr)
{
	if(listen(server_socket,5) == -1)
	{
		return Listening_Fail;
	}
	
	int client_socket;
	int client_addr_size;
	client_addr_size = sizeof(*client_addr);
	
	client_socket = accept(server_socket, (struct sockaddr*)client_addr, &client_addr_size);
	
	if(client_socket == -1)
	{
		return Acception_Fail;
	}
	
	return client_socket;
}


int SendFile(int client_socket)
{
	FILE* fp = fopen("sample1.jpg","rb");
	if(fp == NULL)
	{
		printf("File Load Fail\n");
		exit(1);
	}
	fseek(fp, 0, SEEK_END);
	int fileSize = ftell(fp);
	int leftSize = fileSize;
	int segmentCnt = fileSize / BUFF_SIZE + 1;
	int nowCnt = 0;
	int sendSize = 0;
	fseek(fp, 0, SEEK_SET);
	char data[BUFF_SIZE];
	char startstring[BUFF_SIZE] = "sending\0";
	char receivedString[BUFF_SIZE];
	printf("%d\n",fileSize);
	
	send(client_socket, startstring, sizeof(startstring), 0);
	
	send(client_socket, &fileSize, sizeof(fileSize), 0);
	//send(client_socket, data, strlen(data) + 1, 1);
	int receivedFileSize = 0;
	recv(client_socket, &receivedFileSize, sizeof(receivedFileSize), 0);
	printf("received : %d\n", receivedFileSize);
	if(fileSize != receivedFileSize)
		return -1;
	
	
	
	while(nowCnt++ <= segmentCnt)
	{
		if(leftSize >= BUFF_SIZE)
		{
			sendSize = BUFF_SIZE;
			leftSize -= BUFF_SIZE;
		}
		else if(leftSize < BUFF_SIZE)
		{
			sendSize = leftSize;
		}
		fread((void *)data, 1, BUFF_SIZE, fp);
		write(client_socket, data, BUFF_SIZE);
	}
	
	fclose(fp);
	return 1;
}

int ReceiveCommand(int client_socket)
{
	int Command = 0;
	int a = recv(client_socket, &Command, sizeof(Command), 0);
	if(a < 0)
	{
		printf("Receive Command Fail\n");
		exit(1);
	}
	printf("recv state: %d\n", a);
	return Command;
}

void MoveRobotArm(int command)
{
	if(command == 1)
	{
		printf("Received : Pass\n");
		passCnt++;
		SendCommand("Pass");
	}
	else if(command == -1)
	{
		printf("Received : Fail\n");
		failCnt++;
		SendCommand("Fail");
	}
	else
	{
		printf("Receive Failure");
		exit(1);
	}
}

void setup(){
 
  printf("%s \n", "Raspberry Startup!");
  fflush(stdout);
 
  //get filedescriptor
  
  for(int i=0;i<4;i++)
  {  
	if(fd<0)
	{
		if((fd=serialOpen(device[i], baud))>=0)
		{
			//free(device[i]);
			continue;
		}
	}
			
	

	if(fd2 < 0)
	{
		if((fd2 = serialOpen(device[i], baud)) >= 0)
		{
			//free(device[i]);
			continue;
		}
	}
	//free(device[i]);		
	printf("Arduino Connected\n");
  }
  
  if(fd < 0 || fd2 < 0){
	printf("fd : %d     fd2 : %d\n", fd, fd2);
    fprintf (stderr, "Unable to open serial device \n") ;
    exit(1); //error
  }
 
  //setup GPIO in wiringPi mode
  if (wiringPiSetup () == -1){
    fprintf (stdout, "Unable to start wiringPi: %s\n", strerror (errno)) ;
    exit(1); //error
  }
 
}


void SendCommand(char * command){
  // Pong every 3 seconds
  
    //serialPuts (fd, "Pong!\n");
    // you can also write data from 0-255
    // 65 is in ASCII 'A'
    serialPuts(fd,command);
    serialPuts(fd2,command);
    delay(1300);
}

void initQueue(Queue *queue)
{
	queue->front = queue->rear = NULL;
	
	queue->count = 0;
}

int isEmpty(Queue *queue)
{
	return queue->count == 0;
}

void Enqueue(Queue *queue, int data)
{
	Node *now = (Node *)malloc(sizeof(Node));
	now->data = data;
	now->next = NULL;
	
	if(isEmpty(queue))
	{
		queue->front = now;
	}
	else
	{
		queue->rear->next = now;
	}
	
	queue->rear = now;
	queue->count++;
}

int Dequeue(Queue *queue)
{
	int re = 0;
	Node *now;
	if(isEmpty(queue))
	{
		return re;
	}
	now = queue->front;
	re = now->data;
	queue->front = now->next;
	free(now);
	queue->count--;
	return re;
}
