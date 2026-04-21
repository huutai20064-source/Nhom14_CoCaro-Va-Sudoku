using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public partial class FormCaro : Form
    {
        int size = 15;
        Button[,] btn;
        int[,] board;
        bool playerX = true;

        public FormCaro()
        {
            InitializeComponent();
            KhoiTao();
        }
        void KhoiTao()
        {
            btn = new Button[size, size];
            board = new int[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    btn[i, j] = new Button();
                    btn[i, j].Size = new Size(30, 30);
                    btn[i, j].Location = new Point(i * 30, j * 30);
                    btn[i, j].Font = new Font("Arial", 12, FontStyle.Bold);
                    btn[i, j].Click += Click;
                    pnlBanCo.Controls.Add(btn[i, j]);
                }
        }
        void Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            int x = b.Location.X / 30;
            int y = b.Location.Y / 30;

            if (board[x, y] != 0) return;

            if (playerX)
            {
                b.Text = "X";
                b.ForeColor = Color.Red;
                board[x, y] = 1;
                lblTurn.Text = "Lượt: O";
            }
            else
            {
                b.Text = "O";
                b.ForeColor = Color.Blue;
                board[x, y] = 2;
                lblTurn.Text = "Lượt: X";
            }

            if (CheckWin(x, y))
            {
                MessageBox.Show((playerX ? "X" : "O") + " thắng!");
                ResetGame();
                return;
            }

            playerX = !playerX;
        }
        bool CheckWin(int x, int y)
        {
            int player = board[x, y];
            return Dem(x, y, 1, 0, player) + Dem(x, y, -1, 0, player) >= 4 ||
                   Dem(x, y, 0, 1, player) + Dem(x, y, 0, -1, player) >= 4 ||
                   Dem(x, y, 1, 1, player) + Dem(x, y, -1, -1, player) >= 4 ||
                   Dem(x, y, 1, -1, player) + Dem(x, y, -1, 1, player) >= 4;
        }

        int Dem(int x, int y, int dx, int dy, int player)
        {
            int count = 0;
            int i = x + dx;
            int j = y + dy;

            while (i >= 0 && i < size && j >= 0 && j < size && board[i, j] == player)
            {
                count++;
                i += dx;
                j += dy;
            }
            return count;
        }
        void ResetGame()
        {
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = 0;
                    btn[i, j].Text = "";
                }

            playerX = true;
            lblTurn.Text = "Lượt: X";
        }
    }
}