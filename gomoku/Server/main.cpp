#define _CRT_SECURE_NO_WARNINGS
#include <windows.h>
#include <Winsock.h>
#include <iostream>
#include <sstream>
#include <vector>
#include <map>

using namespace std;

// 판정을 위한 보드에 대한 정보
struct board {
	int gBoard[15][15];
	board() {
		for (int i = 0; i < 15; i++) {
			for (int j = 0; j < 15; j++) {
				gBoard[i][j] = 0;
			}
		}
	}
};

const int size = 33;
const int edge = 15;
enum Horse { none = 0, BLACK, WHITE };

class Client {
private:
	int clientID;
	int roomID;
	SOCKET clientSocket;
	Horse cHorse;

public:
	Client(int clientID, SOCKET clientSocket) {
		this->clientID = clientID;
		this->roomID = -1;
		this->clientSocket = clientSocket;
		cHorse = none;
	}
	void setHorse(Horse h) {
		cHorse = h;
	}
	int getHorse() {
		return cHorse;
	}
	int getClientID() {
		return clientID;
	}
	int getRoomID() {
		return roomID;
	}
	void setRoomID(int roomID) {
		this->roomID = roomID;
	}
	SOCKET getClientSocket() {
		return clientSocket;
	}
};

vector<Client> connections;
map<int, board> roominfo; // 방에 대한 정보를 방번호로 구분짓는다
WSAData wsaData;
SOCKET serv_sock;
SOCKADDR_IN serv_addr;

int nextID;

vector<string> getTokens(string input, char delimiter) {
	vector<string> tokens;
	istringstream f(input);
	string s;
	while (getline(f, s, delimiter)) {
		tokens.push_back(s);
	}
	return tokens;
}

int clientCountInRoom(int roomID) {
	int count = 0;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			count++;
		}
	}
	return count;
}

void playClient(int roomID) {
	char *sent = new char[256];
	bool black = true;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			if (black) {
				// cout << "말 할당 1\n";
				sprintf(sent, "%s", "[Play]Black");
				black = false;
			}
			else {
				// cout << "말 할당 2\n";
				sprintf(sent, "%s", "[Play]White");
			}
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

void exitClient(int roomID) {
	char *sent = new char[256];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			sprintf(sent, "%s", "[Exit]");
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

void putWinClient(int roomID, int sockid) {
	char *sent = new char[256];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			if (connections[i].getClientSocket() == sockid) {
				string data = "[Win]";
				sprintf(sent, "%s", data.c_str());
				send(connections[i].getClientSocket(), sent, 256, 0);
			}
			else {
				string data = "[Lose]";
				sprintf(sent, "%s", data.c_str());
				send(connections[i].getClientSocket(), sent, 256, 0);

			}
		}
	}
}

void putClient(int roomID, int x, int y) {
	char *sent = new char[256];
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			string data = "[Put]" + to_string(x) + "," + to_string(y);
			sprintf(sent, "%s", data.c_str());
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

void chatClient(int socknum, int roomID, string msg) {
	char *sent = new char[256];
	bool ok = false;
	string data;
	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			if (!ok && connections[i].getClientSocket() == socknum) {
				data = "[Chat](Black): " + msg;
				ok = true;
				break;
			}
			else {
				data = "[Chat](White): " + msg;
				break;
			}
		}
	}

	for (int i = 0; i < connections.size(); i++) {
		if (connections[i].getRoomID() == roomID) {
			ZeroMemory(sent, 256);
			sprintf(sent, "%s", data.c_str());
			send(connections[i].getClientSocket(), sent, 256, 0);
		}
	}
}

bool isWin(int roomID, int nowHorse)
{
	// | 오목
	for (int i = 0; i < edge - 4; i++)
	{
		for (int j = 0; j < edge; j++)
		{
			if (roominfo[roomID].gBoard[i][j] == nowHorse && roominfo[roomID].gBoard[i + 1][j] == nowHorse && roominfo[roomID].gBoard[i + 2][j] == nowHorse
				&& roominfo[roomID].gBoard[i + 3][j] == nowHorse && roominfo[roomID].gBoard[i + 4][j] == nowHorse)
			{
				return true;
			}
		}
	}
	// ㅡ 오목
	for (int j = 0; j < edge - 4; j++)
	{
		for (int i = 0; i < edge; i++)
		{
			if (roominfo[roomID].gBoard[i][j] == nowHorse && roominfo[roomID].gBoard[i][j + 1] == nowHorse && roominfo[roomID].gBoard[i][j + 2] == nowHorse
				&& roominfo[roomID].gBoard[i][j + 3] == nowHorse && roominfo[roomID].gBoard[i][j + 4] == nowHorse)
			{
				return true;
			}
		}
	}
	// \ 오목
	for (int i = 0; i < edge - 4; i++)
	{
		for (int j = 0; j < edge - 4; j++)
		{
			if (roominfo[roomID].gBoard[i][j] == nowHorse && roominfo[roomID].gBoard[i + 1][j + 1] == nowHorse && roominfo[roomID].gBoard[i + 2][j + 2] == nowHorse
				&& roominfo[roomID].gBoard[i + 3][j + 3] == nowHorse && roominfo[roomID].gBoard[i + 4][j + 4] == nowHorse)
			{
				return true;
			}
		}
	}
	// / 오목
	for (int i = 0; i < edge - 4; i++)
	{
		for (int j = 0; j < edge - 4; j++)
		{
			if (roominfo[roomID].gBoard[edge - i - 1][j] == nowHorse && roominfo[roomID].gBoard[edge -i - 2][j + 1] == nowHorse && roominfo[roomID].gBoard[edge - i - 3][j + 2] == nowHorse
				&& roominfo[roomID].gBoard[edge - i - 4][j + 3] == nowHorse && roominfo[roomID].gBoard[edge - i - 5][j + 4] == nowHorse)
			{
				return true;
			}
		}
	}
	return false;
}

void ServerThread(Client *client) {
	char *sent = new char[256];
	char *received = new char[256];
	int size = 0;
	while (true) {
		ZeroMemory(received, 256);
		if ((size = recv(client->getClientSocket(), received, 256, NULL)) > 0) {
			string receivedString = string(received);
			vector<string> tokens = getTokens(receivedString, ']');
			if (receivedString.find("[Enter]") != -1) {
				/* 메시지를 보낸 클라이언트를 찾기 */
				for (int i = 0; i < connections.size(); i++) {
					string roomID = tokens[1];
					int roomInt = atoi(roomID.c_str());
					if (connections[i].getClientSocket() == client->getClientSocket()) {
						int clientCount = clientCountInRoom(roomInt);
						/* 2명 이상이 동일한 방에 들어가 있는 경우 가득 찼다고 전송 */
						if (clientCount >= 2) {
							ZeroMemory(sent, 256);
							sprintf(sent, "%s", "[Full]");
							send(connections[i].getClientSocket(), sent, 256, 0);
							break;
						}
						cout << "클라이언트 [" << client->getClientID() << "]: " << roomID << "번 방으로 접속" << endl;
		
						/* 해당 사용자의 방 접속 정보 갱신 */
						Client *newClient = new Client(*client);
						newClient->setRoomID(roomInt);
						connections[i] = *newClient;

						/* 상대방이 이미 방에 들어가 있는 경우 게임 시작 */
						if (clientCount == 1) {
							playClient(roomInt);
							board tmp;
							if (!roominfo.count(roomInt)) {
								roominfo[roomInt] = tmp;
							}
							newClient->setHorse(WHITE);
						}
						/* 혼자 입장한 경우, 검은 말 부여 */
						else {
							newClient->setHorse(BLACK);
						}
						
						client = newClient;
						connections[i] = *newClient;
						/* 방에 성공적으로 접속했다고 메시지 전송 */
						ZeroMemory(sent, 256);
						sprintf(sent, "%s", "[Enter]");
						send(connections[i].getClientSocket(), sent, 256, 0);
					}
				}
			}
			else if (receivedString.find("[Put]") != -1) {
				/* 메시지를 보낸 클라이언트 정보 받기 */
				string data = tokens[1];
				vector<string> dataTokens = getTokens(data, ',');
				int roomID = atoi(dataTokens[0].c_str());
				int x = atoi(dataTokens[1].c_str());
				int y = atoi(dataTokens[2].c_str());
				/* 사용자가 놓은 돌의 위치를 전송 */
				roominfo[client->getRoomID()].gBoard[y][x] = client->getHorse();
				putClient(roomID, x, y);
				
				if (isWin(client->getRoomID(), client->getHorse())) {
					putWinClient(roomID, client->getClientSocket());
					for (int i = 0; i < 15; i++) {
						for (int j = 0; j < 15; j++) {
							roominfo[client->getRoomID()].gBoard[i][j] = none;
						}
					}
				}
			}
			else if (receivedString.find("[Play]") != -1) {
				/* 메시지를 보낸 클라이언트를 찾기 */
				cout << "재시작 메세지가 옴\n";
				string roomID = tokens[1];
				int roomInt = atoi(roomID.c_str());
				for (int i = 0; i < 15; i++) {
					for (int j = 0; j < 15; j++) {
						roominfo[roomInt].gBoard[i][j] = none;
					}
				}
				/* 사용자가 놓은 돌의 위치를 전송 */
				playClient(roomInt);
			}
			else if (receivedString.find("[Chat]") != -1) {
				int sockid = client->getClientSocket();
				int roomInt = client->getRoomID();
				string msg = tokens[1];
				chatClient(sockid, roomInt, msg);
			}
		}
		else {
			ZeroMemory(sent, 256);
			sprintf(sent, "클라이언트 [%i]의 연결이 끊어졌습니다.", client->getClientID());
			cout << sent << endl;
			/* 게임에서 나간 플레이어를 찾기 */
			for (int i = 0; i < connections.size(); i++) {
				if (connections[i].getClientID() == client->getClientID()) {
					/* 다른 사용자와 게임 중이던 사람이 나간 경우 */
					if (connections[i].getRoomID() != -1 &&
						clientCountInRoom(connections[i].getRoomID()) == 2) {
						/* 남아있는 사람에게 메시지 전송 */
						exitClient(connections[i].getRoomID());
					}
					connections.erase(connections.begin() + i);
					break;
				}
			}
			delete client;
			break;
		}
	}
}

int main() {
	// 2.2 버전
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	serv_sock = socket(AF_INET, SOCK_STREAM, NULL);

	serv_addr.sin_addr.s_addr = inet_addr("127.0.0.1");
	serv_addr.sin_port = htons(8000);
	serv_addr.sin_family = AF_INET;

	cout << "## C++ 오목 게임 서버 가동 ##" << endl;
	bind(serv_sock, (SOCKADDR*)&serv_addr, sizeof(serv_addr));
	listen(serv_sock, 32);

	int addressLength = sizeof(serv_addr);
	while (true) {
		SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, NULL);
		if (clientSocket = accept(serv_sock, (SOCKADDR*)&serv_addr, &addressLength)) {
			Client *client = new Client(nextID, clientSocket);
			cout << "[ 새로운 사용자 접속 ]" << endl;
			CreateThread(NULL, NULL, (LPTHREAD_START_ROUTINE)ServerThread, (LPVOID)client, NULL, NULL);
			connections.push_back(*client);
			nextID++;
		}
		Sleep(100);
	}
}