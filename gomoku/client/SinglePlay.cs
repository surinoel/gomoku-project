using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class SinglePlay : Form
    {
        // 오목판의 셀 크기와 선 개수
        // 현재 공식 오목판은 가로 15, 세로 15줄이다
        // board를 500*500으로 잡았기 때문에, 바둑알의 크기는 지름 33으로 잡았다
        private const int size = 33;
        private const int edge = 15;
    
        // enum으로 BLACK = 1, WHITE = 2로 구분
        private enum Horse { none = 0, BLACK, WHITE };

        struct Board
        {
            public Horse state;
            public double weight;
            public bool check;

            public Board(Horse state, double weight, bool check)
            {
                this.state = state;
                this.weight = weight;
                this.check = check;
            }
        }

        // gBoard에 대한 2차원 배열 선언
        private Board[,] gBoard = new Board[edge, edge];
        // 현재 플레이어 선언
        private Horse nowPlayer = Horse.BLACK;
        // 게임 시작 버튼을 넣음으로써 생기는 변수
        private bool nowPlaying = false;

        // AI 처리를 위한 방향변수
        private int[,] dxdy = new int[8, 2] { 
                                    { -1, 1 }, 
                                    { 0, 1 },
                                    { 1, 1 },
                                    { 1, 0 },
                                    { 1, -1 },
                                    { 0, -1 },
                                    { -1, -1 },
                                    { -1, 0 }
        };

        // 클래스 처음에 만들어졌을 때 기본 구성
        public SinglePlay()
        {
            InitializeComponent();
        }

        private bool isWin()
        {
            // | 오목
            for (int i = 0; i < edge - 4; i++) 
            {
                for (int j = 0; j < edge; j++)
                {
                    if(gBoard[i, j].state == nowPlayer && gBoard[i + 1, j].state == nowPlayer && gBoard[i + 2, j].state == nowPlayer
                        && gBoard[i + 3, j].state == nowPlayer && gBoard[i + 4, j].state == nowPlayer)
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
                    if (gBoard[i, j].state == nowPlayer && gBoard[i, j + 1].state == nowPlayer && gBoard[i, j + 2].state == nowPlayer
                        && gBoard[i, j + 3].state == nowPlayer && gBoard[i, j + 4].state == nowPlayer)
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
                    if (gBoard[i, j].state == nowPlayer && gBoard[i + 1, j + 1].state == nowPlayer && gBoard[i + 2, j + 2].state == nowPlayer
                        && gBoard[i + 3, j + 3].state == nowPlayer && gBoard[i + 4, j + 4].state == nowPlayer)
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
                    if (gBoard[edge - i - 1, j].state == nowPlayer && gBoard[edge - i - 2, j + 1].state == nowPlayer && gBoard[edge - i - 3, j + 2].state == nowPlayer
                        && gBoard[edge - i - 4, j + 3].state == nowPlayer && gBoard[edge - i - 5, j + 4].state == nowPlayer)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void judge_AI()
        {
            for (int k = 0; k < 8; k++) 
            {
                for (int i = 0; i < edge; i++) 
                {
                    for (int j = 0; j < edge; j++) 
                    {
                        gBoard[i, j].check= false;
                        gBoard[i, j].weight = 1.0;
                    }
                }

                for (int i = 0; i < edge; i++)
                {
                    for (int j = 0; j < edge; j++)
                    {
                        if(gBoard[i, j].state == Horse.BLACK && gBoard[i, j].check == false)
                        {
                            int cnt = 0;
                            int tx = i, ty = j;
                            while(tx >= 0 && ty >= 0 && tx < edge && ty < edge && gBoard[tx, ty].state == Horse.BLACK)
                            {
                                // 경우의 수를 정한다
                                cnt++;
                                gBoard[tx, ty].check = true;
                                tx += dxdy[k, 0];
                                ty += dxdy[k, 1];
                            }
       
                            if (i - dxdy[k, 0] >= 0 && j - dxdy[k, 1] >= 0 &&
                                i - dxdy[k, 0] < edge && j - dxdy[k, 1] < edge)
                            {
                                if (gBoard[i - dxdy[k, 0], j - dxdy[k, 1]].state == Horse.none &&
                                    gBoard[tx + dxdy[k, 0], ty + dxdy[k, 1]].state == Horse.none)
                                {
                                    gBoard[i - dxdy[k, 0], j - dxdy[k, 1]].weight *= Math.Pow(2.0, cnt * cnt);
                                }
                                else {
                                    gBoard[i - dxdy[k, 0], j - dxdy[k, 1]].weight *= Math.Pow(1.7, cnt * cnt);
                                }

                                if (tx + dxdy[k, 0] >= 0 && ty + dxdy[k, 1] >= 0 &&
                                tx + dxdy[k, 0] < edge && ty + dxdy[k, 1] < edge)
                                {
                                    if (gBoard[i - dxdy[k, 0], j - dxdy[k, 1]].state == Horse.none &&
                                       gBoard[tx + dxdy[k, 0], ty + dxdy[k, 1]].state == Horse.none)
                                    {
                                        gBoard[tx, ty].weight *= Math.Pow(2.0, cnt * cnt);
                                    }
                                    else
                                    {
                                        gBoard[tx, ty].weight *= Math.Pow(1.7, cnt * cnt);
                                    }
                                }
                            }
                         
                        }

                        if (gBoard[i, j].state == Horse.WHITE && gBoard[i, j].check == false)
                        {
                            for (int u = 0; u < 8; u++) 
                            {
                                int x = i + dxdy[u, 0];
                                int y = j + dxdy[u, 1];
                                if (x < 0 || y < 0 || x > edge - 1 || y > edge - 1) continue;
                                gBoard[x, y].weight *= 1.05;
                            }

                            int cnt = 0;
                            int tx = i, ty = j;
                            while (tx >= 0 && ty >= 0 && tx < edge && ty < edge && gBoard[tx, ty].state == Horse.WHITE)
                            {
                                // 경우의 수를 정한다
                                cnt++;
                                gBoard[tx, ty].check = true;
                                tx += dxdy[k, 0];
                                ty += dxdy[k, 1];
                            }

                            if (cnt > 1)
                            {
                                if (i - dxdy[k, 0] >= 0 && j - dxdy[k, 1] >= 0 &&
                                    i - dxdy[k, 0] < edge && j - dxdy[k, 1] < edge)
                                {
                                    gBoard[i - dxdy[k, 0], j - dxdy[k, 1]].weight *= Math.Pow(1.4, cnt * cnt);
                                }
                                if (tx >= 0 && ty >= 0 &&
                                    tx < edge && ty < edge)
                                {
                                    gBoard[tx, ty].weight *= Math.Pow(1.4, cnt * cnt);
                                }
                            }
                        }
                    }
                }
            }
        }

        // 오목판이 refresh 되었을 때 발생하는 이벤트
        private void gomoku_area_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            // 굵기가 2인 펜 객체 생성
            Pen p = new Pen(Color.Black, 2);
            // Point(x, y), 내 개념에서는 x, y가 바뀜
            gp.DrawLine(p, size / 2, size / 2, size / 2, size * edge - size / 2);
            gp.DrawLine(p, size / 2, size / 2, size * edge - size / 2, size / 2);
            gp.DrawLine(p, size / 2, size * edge - size / 2, size * edge - size / 2, size * edge - size / 2);
            gp.DrawLine(p, size * edge - size / 2, size / 2, size * edge - size / 2, size * edge - size / 2);

            // 대각선으로 이동하면서 상하 라인을 그려줌
            for(int i= size + size / 2; i<size * edge - size / 2; i += size)
            {
                gp.DrawLine(p, size / 2, i, size * edge - size / 2, i);
                gp.DrawLine(p, i, size / 2, i, size * edge - size/ 2);
            }
        }

        private void gomoku_area_MouseDown(object sender, MouseEventArgs e)
        {
            if(!nowPlaying)
            {
                MessageBox.Show("게임을 실행한 후 눌러주세요");
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
            if(gBoard[y, x].state != Horse.none)
            {
                MessageBox.Show("이미 말이 놓여진 자리입니다");
                return;
            }
            gBoard[y, x].state = nowPlayer;
            
            // 검은색을 만들기 위해 SolidBrush 객체를 생성
            SolidBrush sb = new SolidBrush(Color.Black);
            g.FillEllipse(sb, x * size, y * size, size, size);

            if (isWin())
            {
                String msg = nowPlayer.ToString() + "플레이어가 승리했습니다";
                MessageBox.Show(msg);
                nowPlaying = false;
                GameStart.Text = "게임시작";
            }

            nowPlayer = ((nowPlayer == Horse.BLACK) ? Horse.WHITE : Horse.BLACK);

            judge_AI();
            double mWeight = -1;
            for (int i = 0; i < edge; i++)
            {
                for (int j = 0; j < edge; j++) 
                {
                    if(gBoard[i, j].weight > mWeight && gBoard[i, j].state == Horse.none)
                    {
                        x = i;
                        y = j;
                        mWeight = gBoard[i, j].weight;
                    }
                }
            }

            gBoard[x, y].state = nowPlayer;
            // 흰색을 만들기 위한 SolidBrush 객체를 생성
            sb = new SolidBrush(Color.White);
            g.FillEllipse(sb, y * size, x * size, size, size);

            // ChatLog.Text = nowPlayer.ToString() + "플레이어 차례입니다";
            nowPlayer = ((nowPlayer == Horse.BLACK) ? Horse.WHITE : Horse.BLACK);
        }

        private void refresh()
        {
            // refresh 함수가 호출되면 Paint 이벤트가 발생함
            this.gomoku_area.Refresh();
            for (int i = 0; i < edge; i++)
            {
                for (int j = 0; j < edge; j++)
                {
                    gBoard[i, j].state = Horse.none;
                }
            }
        }

        private void GameStart_Click(object sender, EventArgs e)
        {
            // 시작버튼을 눌렀을 때
            if(!nowPlaying)
            {
                refresh();
                nowPlaying = true;
                GameStart.Text = "RESTART";
                ChatLog.Text = nowPlayer.ToString() + "플레이어의 차례입니다";
            }
            else
            {
                refresh();
                ChatLog.Text = "게임이 재시작 되었습니다";
            }
        }
    }
}