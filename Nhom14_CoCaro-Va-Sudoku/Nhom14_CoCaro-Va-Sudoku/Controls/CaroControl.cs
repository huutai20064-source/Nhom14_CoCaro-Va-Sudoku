    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Data.SqlClient;

    namespace Nhom14_CoCaro_Va_Sudoku
    {
        public class CaroControl : UserControl
        {
            int size = 15;
            Button[,] btn;
            int[,] board;
            bool playerX = true;

            Panel pnlBanCo;
            Label lblTurn;
            Button btnReset;

            int cellSize = 30;
            int offsetX, offsetY;

            bool hasWinLine = false;
            Point winStart, winEnd;

            SqlConnection conn;
            int nguoiChoiId = 1;

            public CaroControl()
            {
                this.Size = new Size(520, 520);
                TaoUI();
                KhoiTao();
                KetNoi();
                LayNguoiChoi();
                pnlBanCo.Paint += PnlBanCo_Paint;
            }

            void KetNoi()
            {
                conn = new SqlConnection("Data Source=.;Initial Catalog=GameCenter;Integrated Security=True;");
            }

            void LayNguoiChoi()
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id FROM NguoiChoi", conn);
                SqlDataReader r = cmd.ExecuteReader();
                if (r.Read()) nguoiChoiId = (int)r["Id"];
                r.Close();
                conn.Close();
            }

            void LuuTran(string kq)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TranCaro(NguoiChoiId, KetQua) VALUES (@id,@kq)", conn);
                cmd.Parameters.AddWithValue("@id", nguoiChoiId);
                cmd.Parameters.AddWithValue("@kq", kq);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            void CapNhatDiem(bool thang)
            {
                conn.Open();
                SqlCommand cmd;

                if (thang)
                {
                    cmd = new SqlCommand("UPDATE NguoiChoi SET DiemCaro = DiemCaro + 10, SoTranThang = SoTranThang + 1 WHERE Id=@id", conn);
                }
                else
                {
                    cmd = new SqlCommand("UPDATE NguoiChoi SET SoTranThua = SoTranThua + 1 WHERE Id=@id", conn);
                }

                cmd.Parameters.AddWithValue("@id", nguoiChoiId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            void TaoUI()
            {
                pnlBanCo = new Panel();
                pnlBanCo.Location = new Point(20, 60);
                pnlBanCo.Size = new Size(450, 450);
                pnlBanCo.BorderStyle = BorderStyle.FixedSingle;
                Controls.Add(pnlBanCo);

                lblTurn = new Label();
                lblTurn.Text = "Lượt đi: X";
                lblTurn.Location = new Point(130, 15);
                lblTurn.Font = new Font("Arial", 14, FontStyle.Bold);
                lblTurn.AutoSize = true;
                Controls.Add(lblTurn);

                btnReset = new Button();
                btnReset.Text = "Chơi lại";
                btnReset.Location = new Point(380, 10);
                btnReset.Click += BtnReset_Click;
                Controls.Add(btnReset);
            }

            void KhoiTao()
            {
                btn = new Button[size, size];
                board = new int[size, size];

                int boardSize = size * cellSize;
                offsetX = (pnlBanCo.Width - boardSize) / 2;
                offsetY = (pnlBanCo.Height - boardSize) / 2;

                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        btn[i, j] = new Button();
                        btn[i, j].Size = new Size(cellSize, cellSize);
                        btn[i, j].Location = new Point(offsetX + i * cellSize, offsetY + j * cellSize);
                        btn[i, j].Font = new Font("Arial", 10, FontStyle.Bold);
                        btn[i, j].Click += OCo_Click;
                        pnlBanCo.Controls.Add(btn[i, j]);
                    }
            }

            void OCo_Click(object sender, EventArgs e)
            {
                Button b = sender as Button;
                int x = (b.Location.X - offsetX) / cellSize;
                int y = (b.Location.Y - offsetY) / cellSize;

                if (board[x, y] != 0) return;

                if (playerX)
                {
                    b.Text = "X";
                    b.ForeColor = Color.Red;
                    board[x, y] = 1;
                }
                else
                {
                    b.Text = "O";
                    b.ForeColor = Color.Blue;
                    board[x, y] = 2;
                }

                if (CheckWin(x, y))
                {
                    MessageBox.Show((playerX ? "X" : "O") + " thắng!");

                    LuuTran("Win");
                    CapNhatDiem(true);

                    ResetGame();
                    return;
                }

                playerX = !playerX;
                lblTurn.Text = "Lượt đi: " + (playerX ? "X" : "O");
            }

            bool CheckWin(int x, int y)
            {
                int p = board[x, y];
                return CheckDir(x, y, 1, 0, p) ||
                       CheckDir(x, y, 0, 1, p) ||
                       CheckDir(x, y, 1, 1, p) ||
                       CheckDir(x, y, 1, -1, p);
            }

            bool CheckDir(int x, int y, int dx, int dy, int p)
            {
                List<Point> list = new List<Point>();
                list.Add(new Point(x, y));

                int i = x + dx;
                int j = y + dy;

                while (i >= 0 && i < size && j >= 0 && j < size && board[i, j] == p)
                {
                    list.Add(new Point(i, j));
                    i += dx;
                    j += dy;
                }

                i = x - dx;
                j = y - dy;

                while (i >= 0 && i < size && j >= 0 && j < size && board[i, j] == p)
                {
                    list.Add(new Point(i, j));
                    i -= dx;
                    j -= dy;
                }

                if (list.Count >= 5)
                {
                    foreach (var pnt in list)
                        btn[pnt.X, pnt.Y].BackColor = Color.Gold;

                    winStart = btn[list[0].X, list[0].Y].Location;
                    winEnd = btn[list[4].X, list[4].Y].Location;

                    hasWinLine = true;
                    pnlBanCo.Invalidate();
                    return true;
                }

                return false;
            }

            void PnlBanCo_Paint(object sender, PaintEventArgs e)
            {
                if (!hasWinLine) return;

                Pen pen = new Pen(Color.Red, 4);

                int x1 = winStart.X + cellSize / 2;
                int y1 = winStart.Y + cellSize / 2;

                int x2 = winEnd.X + cellSize / 2;
                int y2 = winEnd.Y + cellSize / 2;

                e.Graphics.DrawLine(pen, x1, y1, x2, y2);
            }

            void ResetGame()
            {
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                    {
                        board[i, j] = 0;
                        btn[i, j].Text = "";
                        btn[i, j].BackColor = SystemColors.Control;
                    }

                playerX = true;
                lblTurn.Text = "Lượt đi: X";
                hasWinLine = false;
                pnlBanCo.Invalidate();
            }

            void BtnReset_Click(object sender, EventArgs e)
            {
                ResetGame();
            }
        }
    }