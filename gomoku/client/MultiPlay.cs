using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 소켓 모듈
using System.Net;
using System.Net.Sockets;

// 스레드 모듈
using System.Threading;

namespace client
{
    public partial class MultiPlay : Form
    {
        private Thread thread; // 통신을 위한 쓰레드
        private TcpClient tcpClient;// TCP 클라이언트
        private NetworkStream stream;

        // 오목판의 셀 크기와 선 개수
        // 현재 공식 오목판은 가로 15, 세로 15줄이다
        // board를 500*500으로 잡았기 때문에, 바둑알의 크기는 지름 33으로 잡았다
        private const int size = 33;
        private const int edge = 15;

        // enum으로 BLACK = 1, WHITE = 2로 구분
        private enum Horse { none = 0, BLACK, WHITE };
        // gBoard에 대한 2차원 배열 선언
        private Horse[,] gBoard;
        // 현재 플레이어 선언
        private Horse nowPlayer = Horse.BLACK;
        private Horse enemyPlayer = Horse.none;

        // 게임 시작 버튼을 넣음으로써 생기는 변수
        private bool nowPlaying = false;
        // 적과의 대치에서 턴의 정보를 알 수 있는 변수
        private bool nowTurn = false;

        private bool entered = false;
        private bool threading = false;

        private int blackTimer_left;
        private int whiteTimer_left;

        private int ex, ey;

        public MultiPlay()
        {
            InitializeComponent();
            // 변수 초기화
            GameStart.Enabled = false;
            nowPlaying = false;
            nowTurn = false;
            entered = false;
            threading = false;
            gBoard = new Horse[edge, edge];
            blackTimer_label.Text = "30";
            whiteTimer_label.Text = "30";

            blackTimer_left = 30;
            whiteTimer_left = 30;
        }

        private void refresh()
        {
            // refresh 함수가 호출되면 Paint 이벤트가 발생함
            this.gomoku_area.Refresh();
            for (int i = 0; i < edge; i++)
            {
                for (int j = 0; j < edge; j++)
                {
                    gBoard[i, j] = Horse.none;
                }
            }
            GameStart.Enabled = false;
        }
#if false
        private bool isWin(Horse nowPlayer)
        {
            // | 오목
            for (int i = 0; i < edge - 4; i++)
            {
                for (int j = 0; j < edge; j++)
                {
                    if (gBoard[i, j] == nowPlayer && gBoard[i + 1, j] == nowPlayer && gBoard[i + 2, j] == nowPlayer
                        && gBoard[i + 3, j] == nowPlayer && gBoard[i + 4, j] == nowPlayer)
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
                    if (gBoard[i, j] == nowPlayer && gBoard[i, j + 1] == nowPlayer && gBoard[i, j + 2] == nowPlayer
                        && gBoard[i, j + 3] == nowPlayer && gBoard[i, j + 4] == nowPlayer)
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
                    if (gBoard[i, j] == nowPlayer && gBoard[i + 1, j + 1] == nowPlayer && gBoard[i + 2, j + 2] == nowPlayer
                        && gBoard[i + 3, j + 3] == nowPlayer && gBoard[i + 4, j + 4] == nowPlayer)
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
                    if (gBoard[edge - i - 1, j] == nowPlayer && gBoard[edge - i - 2, j + 1] == nowPlayer && gBoard[edge - i - 3, j + 2] == nowPlayer
                        && gBoard[edge - i - 4, j + 3] == nowPlayer && gBoard[edge - i - 5, j + 4] == nowPlayer)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
#endif
        private void Enter_Button_Click(object sender, EventArgs e)
        {
            tcpClient = new TcpClient();
            // string Ipv4 = getDNStoIP("0.tcp.ngrok.io");
            tcpClient.Connect("127.0.0.1", 8000);
            stream = tcpClient.GetStream();

            // client에 대한 스레드 생성, 함수는 read
            thread = new Thread(new ThreadStart(read));
            thread.Start();
            threading = true;

            string msg = "[Enter]";
            byte[] buf = Encoding.ASCII.GetBytes(msg + this.Room.Text);
            stream.Write(buf, 0, buf.Length);
        }

        private void GameStart_Click(object sender, EventArgs e)
        {
            // 시작버튼을 눌렀을 때
            if (!nowPlaying)
            {
                refresh();
                nowPlaying = true;
                GameStart.Text = "재시작";
                string msg = "[Play]";
                byte[] buf = Encoding.ASCII.GetBytes(msg + this.Room.Text);
                stream.Write(buf, 0, buf.Length);
                this.Status.Text += "상대 플레이어의 준비를 기다립니다.\r\n";
                this.GameStart.Enabled = false;
            }
        }

        private void read()
        {
            while (true)
            {
                byte[] buf = new byte[1024];
                int bufBytes = stream.Read(buf, 0, buf.Length);
                string msg = Encoding.UTF8.GetString(buf, 0, bufBytes);
                // 접속이 성공한 경우
                if (msg.Contains("[Enter]"))
                {
                    this.Status.Text += "[" + this.Room.Text + "]번 방에 접속했습니다.\r\n";
                    /* 게임 시작 처리 */
                    this.Room.Enabled = false;
                    this.Enter_Button.Enabled = false;
                    entered = true;
                }
                // 방이 가득 찬 경우
                if (msg.Contains("[Full]"))
                {
                    this.Status.Text += "이미 가득 찬 방입니다.\r\n";
                    closeNetwork();
                }
                // 게임이 시작된 경우
                if (msg.Contains("[Play]"))
                {
                    refresh();
                    string horse = msg.Split(']')[1];
                    if (horse.Contains("Black"))
                    {
                        this.Status.Text += "당신의 차례입니다.\r\n";
                        nowTurn = true;
                        nowPlayer = Horse.BLACK;
                        enemyPlayer = Horse.WHITE;
                    }
                    else
                    {
                        this.Status.Text += "상대방의 차례입니다.\r\n";
                        nowTurn = false;
                        nowPlayer = Horse.WHITE;
                        enemyPlayer = Horse.BLACK;
                    }
                    nowPlaying = true;

                    blackTimer.Start();
                }
                // 상대방이 나간 경우
                if (msg.Contains("[Exit]"))
                {
                    this.Status.Text += "상대방이 나갔습니다.\r\n";
                    refresh();
                }
                if (msg.Contains("[Put]"))
                {
                    string position = msg.Split(']')[1];
                    int x = Convert.ToInt32(position.Split(',')[0]);
                    int y = Convert.ToInt32(position.Split(',')[1]);
                    
                    if (gBoard[x, y] != Horse.none) continue;
                    gBoard[x, y] = enemyPlayer;
                    Graphics g = this.gomoku_area.CreateGraphics();
                    if (enemyPlayer == Horse.BLACK)
                    {
                        SolidBrush brush = new SolidBrush(Color.SandyBrown);
                        g.FillRectangle(brush, ex * size, ey * size, 4, 4);
                        brush = new SolidBrush(Color.Red);
                        g.FillEllipse(brush, x * size, y * size, 4, 4);
                        brush = new SolidBrush(Color.Black);
                        g.FillEllipse(brush, x * size, y * size, size, size);

                        ex = x; ey = y;
                    }
                    else
                    {
                        SolidBrush brush = new SolidBrush(Color.SandyBrown);
                        g.FillRectangle(brush, ex * size, ey * size, 4, 4);
                        brush = new SolidBrush(Color.Red);
                        g.FillEllipse(brush, x * size, y * size, 4, 4);
                        brush = new SolidBrush(Color.White);
                        g.FillEllipse(brush, x * size, y * size, size, size);

                        ex = x; ey = y;
                    }
#if false
                    if (isWin(enemyPlayer))
                    {
                        Status.Text = "패배했습니다.";
                        nowPlaying = false;
                        GameStart.Text = "재시작";
                        GameStart.Enabled = true;
                    }
#endif
                    Status.AppendText("당신이 둘 차례입니다.\r\n");
                    nowTurn = true;

                    if(nowPlayer == Horse.BLACK)
                    {
                        whiteTimer.Stop();
                        whiteTimer_label.Text = "30";
                        whiteTimer_left = 30;

                        blackTimer_label.Text = "30";
                        blackTimer_left = 30;

                        blackTimer.Start();
                    }
                    else
                    {
                        blackTimer.Stop();
                        blackTimer_label.Text = "30";
                        blackTimer_left = 30;

                        whiteTimer_label.Text = "30";
                        whiteTimer_left = 30;

                        whiteTimer.Start();
                    }
                }
                if (msg.Contains("[Win]"))
                {
                    Status.AppendText("승리했습니다.\r\n");
                    nowPlaying = false;
                    GameStart.Text = "재시작";
                    GameStart.Enabled = true;

                    blackTimer.Stop();
                    blackTimer_label.Text = "30";
                    blackTimer_left = 30;

                    whiteTimer.Stop();
                    whiteTimer_label.Text = "30";
                    whiteTimer_left = 30;
                }
                if (msg.Contains("[Lose]"))
                {
                    Status.AppendText("패배했습니다.\r\n");
                    nowPlaying = false;
                    GameStart.Text = "재시작";
                    GameStart.Enabled = true;

                    blackTimer.Stop();
                    blackTimer_label.Text = "30";
                    blackTimer_left = 30;

                    whiteTimer.Stop();
                    whiteTimer_label.Text = "30";
                    whiteTimer_left = 30;
                }
                if(msg.Contains("[Chat]"))
                {
                    string log = msg.Split(']')[1];
                    Status.AppendText(log);
                    Status.Text += Environment.NewLine;
                }
            }
        }
        // 바둑판이 눌러졌을 때
        private void gomoku_area_MouseDown(object sender, MouseEventArgs e)
        {
            if (!nowPlaying) // 혼자 입장해있을 때
            {
                MessageBox.Show("게임을 실행한 후 눌러주세요");
                return;
            }
            if (!nowTurn) // 내 턴이 아니라면
            {
                return;
            }
            // 오목판에 그림을 그리기 위해 Graphics 객체를 만들어줬다
            Graphics g = this.gomoku_area.CreateGraphics();
            int x = e.X / size;
            int y = e.Y / size;
            // 테두리를 벗어났을 때
            if (x < 0 || y < 0 || x >= edge || y >= edge)
            {
                MessageBox.Show("테두리를 벗어났습니다");
                return;
            }
            // 클릭한 좌표 출력
            // MessageBox.Show(x + ", " + y);
            // 검은색 말이라면
            if (gBoard[x, y] != Horse.none) return;

            gBoard[x, y] = nowPlayer;

            string message = "[Put]" + Room.Text + "," + x + "," + y;
            byte[] buf = Encoding.ASCII.GetBytes(message);
            stream.Write(buf, 0, buf.Length);

            if (nowPlayer == Horse.BLACK)
            {
                // 검은색을 만들기 위해 SolidBrush 객체를 생성
                SolidBrush sb = new SolidBrush(Color.Black);
                g.FillEllipse(sb, x * size, y * size, size, size);
            }
            else
            {
                // 흰색을 만들기 위한 SolidBrush 객체를 생성
                SolidBrush sb = new SolidBrush(Color.White);
                g.FillEllipse(sb, x * size, y * size, size, size);
            }
#if false
                if (isWin(nowPlayer))
                {
                    String msg = nowPlayer.ToString() + "플레이어가 승리했습니다";
                    MessageBox.Show(msg);
                    nowPlaying = false;
                    GameStart.Text = "재시작";
                }
#endif
            Status.Text += "상대방 플레이어 차례입니다.\r\n";
            nowTurn = false;

            if(enemyPlayer == Horse.WHITE)
            {
                blackTimer.Stop();
                blackTimer_label.Text = "30";
                blackTimer_left = 30;

                whiteTimer_label.Text = "30";
                whiteTimer_left = 30;

                whiteTimer.Start();
            }
            else
            {
                whiteTimer.Stop();
                whiteTimer_label.Text = "30";
                whiteTimer_left = 30;

                blackTimer_label.Text = "30";
                blackTimer_left = 30;

                blackTimer.Start();
            }
        }

            // 화면 구성 함수
            private void gomoku_area_Paint(object sender, PaintEventArgs e)
            {
                Graphics gp = e.Graphics;
                // 굵기가 3인 펜 객체 생성
                Pen p = new Pen(Color.Black, 2);
                // Point(x, y), 내 개념에서는 x, y가 바뀜
                gp.DrawLine(p, size / 2, size / 2, size / 2, size * edge - size / 2);
                gp.DrawLine(p, size / 2, size / 2, size * edge - size / 2, size / 2);
                gp.DrawLine(p, size / 2, size * edge - size / 2, size * edge - size / 2, size * edge - size / 2);
                gp.DrawLine(p, size * edge - size / 2, size / 2, size * edge - size / 2, size * edge - size / 2);

                p = new Pen(Color.Black, 1);
                // 대각선으로 이동하면서 상하 라인을 그려줌
                for (int i = size + size / 2; i < size * edge - size / 2; i += size)
                {
                    gp.DrawLine(p, size / 2, i, size * edge - size / 2, i);
                    gp.DrawLine(p, i, size / 2, i, size * edge - size / 2);
                }
            }

            // C#에서 domain -> DNS로 바꿔주는 함수
            public static string getDNStoIP(string host)
            {
                IPHostEntry hostInfo;
                try
                {
                    hostInfo = Dns.Resolve(host);
                }
                catch (Exception e)
                {
                    return "Unable to resolve host\n";
                }
                return hostInfo.AddressList[0].ToString();
            }

            // MultiPlay 창이 닫혀졌을 때
            private void MultiPlay_FormClosed(object sender, FormClosedEventArgs e)
            {
                closeNetwork();
            }

            void closeNetwork()
            {
                if (threading && thread.IsAlive) thread.Abort();
                if (entered)
                {
                    tcpClient.Close();
                }
            }

        private void send_Click(object sender, EventArgs e)
        {
            string msg = "[Chat]";
            byte[] buf = Encoding.UTF8.GetBytes(msg + this.comment.Text);
            this.comment.Clear();
            stream.Write(buf, 0, buf.Length);
        }

        private void comment_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string msg = "[Chat]";
                byte[] buf = Encoding.UTF8.GetBytes(msg + this.comment.Text);
                this.comment.Clear();
                stream.Write(buf, 0, buf.Length);
            }
        }

        private void whiteTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (whiteTimer_left > 0)
            {
                whiteTimer_left -= 1;
                whiteTimer_label.Text = whiteTimer_left.ToString();
            }
            else
            {
                if(nowPlayer == Horse.WHITE)
                {
                    // MessageBox.Show("주어진 시간을 초과했습니다. 주의해주세요!");
                    this.Status.Text += "시간 초과입니다 주의해주세요.\r\n";
                }
                whiteTimer_left = 30;
                whiteTimer.Stop();
                whiteTimer_label.Text = "30";
                whiteTimer.Start();
            }
        }
        
        private void blackTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (blackTimer_left > 0)
            {
                blackTimer_left -= 1;
                blackTimer_label.Text = blackTimer_left.ToString();
            }
            else
            {
                if(nowPlayer == Horse.BLACK)
                {
                    // MessageBox.Show("주어진 시간을 초과했습니다. 주의해주세요!");
                    this.Status.Text += "시간 초과입니다 주의해주세요.\r\n";
                }
                blackTimer_left = 30;
                blackTimer.Stop();
                blackTimer_label.Text = "30";
                blackTimer.Start();
            }
        }
    }
}