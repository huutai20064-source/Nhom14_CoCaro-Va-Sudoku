using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Nhom14_CoCaro_Va_Sudoku.BUS;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public class CaroControl : UserControl
    {
        int size = 15;
        Button[,] btn;

        Panel pnlBanCo;
        Label lblTurn;
        Button btnReset;
        Label lblTitle;

        int cellSize;
        int offsetX, offsetY;

        bool playerX = true;
        bool gameOver = false;

        bool aiMode;
        int aiLevel;

        CaroEngine engine = new CaroEngine();
        CaroAI ai = new CaroAI();
        CaroBUS bus = new CaroBUS();

        int nguoiChoiId;

        public CaroControl(bool aiMode = false, int level = 1)
        {
            this.aiMode = aiMode;
            this.aiLevel = level;

            this.DoubleBuffered = true;
            this.Size = new Size(850, 760);
            this.BackColor = Color.FromArgb(20, 25, 40);

            TaoUI();
            KhoiTao();

            nguoiChoiId = bus.LayNguoiChoi();

            this.Resize += (s, e) => UpdateLayout();
        }

        void TaoUI()
        {
            lblTitle = new Label();
            lblTitle.Text = "GAME CARO";
            lblTitle.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            Controls.Add(lblTitle);

            lblTurn = new Label();
            lblTurn.Text = "Lượt: X";
            lblTurn.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTurn.ForeColor = Color.White;
            lblTurn.AutoSize = true;
            Controls.Add(lblTurn);

            pnlBanCo = new Panel();
            pnlBanCo.BackColor = Color.FromArgb(35, 40, 60);
            pnlBanCo.BorderStyle = BorderStyle.None;
            pnlBanCo.Paint += PnlBanCo_Paint;
            Controls.Add(pnlBanCo);

            btnReset = new Button();
            btnReset.Text = "CHƠI LẠI";
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.BackColor = Color.FromArgb(0, 180, 120);
            btnReset.ForeColor = Color.White;
            btnReset.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnReset.Cursor = Cursors.Hand;
            btnReset.Size = new Size(140, 45);

            btnReset.MouseEnter += (s, e) =>
            {
                btnReset.BackColor = Color.FromArgb(0, 210, 140);
            };

            btnReset.MouseLeave += (s, e) =>
            {
                btnReset.BackColor = Color.FromArgb(0, 180, 120);
            };

            btnReset.Click += (s, e) => ResetGame();

            Controls.Add(btnReset);
        }

        void KhoiTao()
        {
            btn = new Button[size, size];

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    btn[x, y] = new Button();

                    btn[x, y].FlatStyle = FlatStyle.Flat;
                    btn[x, y].FlatAppearance.BorderSize = 0;

                    btn[x, y].BackColor = Color.FromArgb(50, 55, 75);
                    btn[x, y].ForeColor = Color.White;

                    btn[x, y].Cursor = Cursors.Hand;

                    btn[x, y].Click += OCo_Click;

                    btn[x, y].MouseEnter += OCo_MouseEnter;
                    btn[x, y].MouseLeave += OCo_MouseLeave;

                    pnlBanCo.Controls.Add(btn[x, y]);
                }
        }

        void OCo_MouseEnter(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b.Text == "")
                b.BackColor = Color.FromArgb(70, 75, 95);
        }

        void OCo_MouseLeave(object sender, EventArgs e)
        {
            Button b = sender as Button;

            if (b.Text == "")
                b.BackColor = Color.FromArgb(50, 55, 75);
        }

        void OCo_Click(object sender, EventArgs e)
        {
            if (gameOver) return;

            Button b = sender as Button;
            if (b.Text != "") return;

            int x = (b.Left - offsetX) / cellSize;
            int y = (b.Top - offsetY) / cellSize;

            int player = playerX ? 1 : 2;

            if (!engine.Danh(x, y, player)) return;

            b.Text = playerX ? "X" : "O";
            b.ForeColor = playerX
                ? Color.FromArgb(255, 80, 80)
                : Color.FromArgb(80, 170, 255);

            if (CheckWin(x, y, player)) return;

            if (aiMode && player == 1)
            {
                var (ax, ay) = ai.NuocDi(engine.board, size, aiLevel);

                if (engine.board[ax, ay] != 0) return;

                engine.Danh(ax, ay, 2);

                btn[ax, ay].Text = "O";
                btn[ax, ay].ForeColor = Color.FromArgb(80, 170, 255);

                if (CheckWin(ax, ay, 2)) return;

                playerX = true;
                lblTurn.Text = "Lượt: X";
            }
            else
            {
                playerX = !playerX;
                lblTurn.Text = "Lượt: " + (playerX ? "X" : "O");
            }
        }

        bool CheckWin(int x, int y, int player)
        {
            if (engine.CheckWin(x, y, out List<Point> list))
            {
                foreach (var p in list)
                {
                    btn[p.X, p.Y].BackColor = Color.Gold;
                    btn[p.X, p.Y].ForeColor = Color.Black;
                }

                gameOver = true;

                bool nguoiThang = (player == 1);

                MessageBox.Show(
                    (nguoiThang ? "X" : "O") + " thắng!",
                    "Kết thúc",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                if (aiMode)
                {
                    if (nguoiThang)
                        bus.Thang(nguoiChoiId, aiLevel, true);
                    else
                        bus.Thua(nguoiChoiId, aiLevel, true);
                }

                return true;
            }

            return false;
        }

        void ResetGame()
        {
            engine.Reset();

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    btn[x, y].Text = "";
                    btn[x, y].BackColor = Color.FromArgb(50, 55, 75);
                    btn[x, y].ForeColor = Color.White;
                }

            gameOver = false;
            playerX = true;

            lblTurn.Text = "Lượt: X";
        }

        void UpdateLayout()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            lblTitle.Left = (w - lblTitle.Width) / 2;
            lblTitle.Top = 10;

            lblTurn.Left = (w - lblTurn.Width) / 2;
            lblTurn.Top = 60;

            int boardSize = Math.Min(w - 80, h - 220);

            pnlBanCo.Size = new Size(boardSize, boardSize);
            pnlBanCo.Left = (w - boardSize) / 2;
            pnlBanCo.Top = 110;

            cellSize = boardSize / size;

            if (cellSize < 20)
                cellSize = 20;

            offsetX = (boardSize - cellSize * size) / 2;
            offsetY = (boardSize - cellSize * size) / 2;

            for (int x = 0; x < size; x++)
                for (int y = 0; y < size; y++)
                {
                    btn[x, y].Size = new Size(cellSize - 3, cellSize - 3);

                    btn[x, y].Location = new Point(
                        offsetX + x * cellSize,
                        offsetY + y * cellSize
                    );

                    float fontSize = Math.Max(9f, cellSize / 2.8f);

                    btn[x, y].Font = new Font(
                        "Segoe UI",
                        fontSize,
                        FontStyle.Bold
                    );
                }

            btnReset.Top = pnlBanCo.Bottom + 20;
            btnReset.Left = (w - btnReset.Width) / 2;
        }

        void PnlBanCo_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.FromArgb(70, 80, 110), 1))
            {
                for (int i = 0; i <= size; i++)
                {
                    g.DrawLine(
                        pen,
                        offsetX,
                        offsetY + i * cellSize,
                        offsetX + size * cellSize,
                        offsetY + i * cellSize
                    );

                    g.DrawLine(
                        pen,
                        offsetX + i * cellSize,
                        offsetY,
                        offsetX + i * cellSize,
                        offsetY + size * cellSize
                    );
                }
            }
        }
    }
}