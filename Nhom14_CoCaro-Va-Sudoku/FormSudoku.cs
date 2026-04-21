using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public partial class FormSudoku : Form
    {
        TextBox[,] txt = new TextBox[9, 9];
        int[,] board = new int[9, 9];

        Button[] btnSo = new Button[9];
        int soDangChon = 0;

        Random rand = new Random(Environment.TickCount);
        int level = 1;

        bool daThongBao = false;

        public FormSudoku()
        {
            InitializeComponent();
            TaoLuoi();
            TaoBangSo();
            TaoMoiGame();
        }

        // ================= GRID =================
        void TaoLuoi()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j] = new TextBox();
                    txt[i, j].Size = new Size(48, 48);
                    txt[i, j].Location = new Point(30 + i * 48, 60 + j * 48);
                    txt[i, j].TextAlign = HorizontalAlignment.Center;
                    txt[i, j].Font = new Font("Segoe UI", 16, FontStyle.Bold);

                    txt[i, j].MaxLength = 1;
                    txt[i, j].ShortcutsEnabled = false;
                    txt[i, j].BorderStyle = BorderStyle.FixedSingle;

                    txt[i, j].KeyPress += NhapSo;

                    txt[i, j].BackColor =
                        (i / 3 + j / 3) % 2 == 0
                        ? Color.FromArgb(235, 245, 255)
                        : Color.White;

                    Controls.Add(txt[i, j]);
                }
        }

        // ================= NUMBER PANEL =================
        void TaoBangSo()
        {
            for (int i = 0; i < 9; i++)
            {
                btnSo[i] = new Button();
                btnSo[i].Text = (i + 1).ToString();
                btnSo[i].Size = new Size(40, 40);
                btnSo[i].Location = new Point(30 + i * 45, 520);
                btnSo[i].Font = new Font("Segoe UI", 12, FontStyle.Bold);
                btnSo[i].BackColor = Color.LightGray;

                int so = i + 1;
                btnSo[i].Click += (s, e) =>
                {
                    soDangChon = so;
                    ToSangSo();
                };

                Controls.Add(btnSo[i]);
            }
        }

        // ================= HIGHLIGHT NUMBER =================
        void ToSangSo()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (txt[i, j].Text != "" &&
                        int.Parse(txt[i, j].Text) == soDangChon)
                    {
                        txt[i, j].BackColor = Color.Gold;
                    }
                    else
                    {
                        txt[i, j].BackColor =
                            (i / 3 + j / 3) % 2 == 0
                            ? Color.FromArgb(235, 245, 255)
                            : Color.White;
                    }
                }
        }

        // ================= GAME =================
        void TaoMoiGame()
        {
            board = new int[9, 9];
            daThongBao = false;

            Solve(0, 0);

            int remove = 30;
            if (level == 2) remove = 45;
            if (level == 3) remove = 55;

            while (remove > 0)
            {
                int i = rand.Next(9);
                int j = rand.Next(9);

                if (board[i, j] != 0)
                {
                    board[i, j] = 0;
                    remove--;
                }
            }

            HienThi();
        }

        // ================= BACKTRACK =================
        bool Solve(int r, int c)
        {
            if (r == 9) return true;
            if (c == 9) return Solve(r + 1, 0);
            if (board[r, c] != 0) return Solve(r, c + 1);

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 9; i++)
            {
                int j = rand.Next(9);
                int t = nums[i];
                nums[i] = nums[j];
                nums[j] = t;
            }

            for (int k = 0; k < 9; k++)
            {
                int num = nums[k];

                if (HopLe(r, c, num))
                {
                    board[r, c] = num;

                    if (Solve(r, c + 1)) return true;

                    board[r, c] = 0;
                }
            }

            return false;
        }

        bool HopLe(int r, int c, int num)
        {
            for (int i = 0; i < 9; i++)
                if (board[r, i] == num || board[i, c] == num)
                    return false;

            int sr = r / 3 * 3;
            int sc = c / 3 * 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[sr + i, sc + j] == num)
                        return false;

            return true;
        }

        // ================= DISPLAY =================
        void HienThi()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j].Enabled = true;

                    if (board[i, j] != 0)
                    {
                        txt[i, j].Text = board[i, j].ToString();
                        txt[i, j].Enabled = false;
                        txt[i, j].ForeColor = Color.Black;
                    }
                    else
                    {
                        txt[i, j].Text = "";
                    }

                    txt[i, j].BackColor =
                        (i / 3 + j / 3) % 2 == 0
                        ? Color.FromArgb(235, 245, 255)
                        : Color.White;
                }
        }

        // ================= INPUT =================
        void NhapSo(object sender, KeyPressEventArgs e)
        {
            TextBox t = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && (e.KeyChar < '1' || e.KeyChar > '9'))
            {
                e.Handled = true;
                return;
            }

            if (!char.IsControl(e.KeyChar))
            {
                t.Text = e.KeyChar.ToString();
                t.SelectionStart = t.Text.Length;
                e.Handled = true;
            }

            ResetMau();
            DanhDauLoi();

            if (!daThongBao && KiemTraHoanThanh())
            {
                daThongBao = true;
                MessageBox.Show("🎉 Chúc mừng! Bạn đã hoàn thành Sudoku!", "Victory");
            }
        }

        // ================= ERROR =================
        void DanhDauLoi()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (txt[i, j].Text == "") continue;

                    int val;
                    if (!int.TryParse(txt[i, j].Text, out val))
                        continue;

                    if (!HopLeCheck(i, j, val))
                        txt[i, j].BackColor = Color.LightCoral;
                }
        }

        void ResetMau()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j].BackColor =
                        (i / 3 + j / 3) % 2 == 0
                        ? Color.FromArgb(235, 245, 255)
                        : Color.White;
                }
        }

        // ================= CHECK =================
        bool HopLeCheck(int r, int c, int num)
        {
            for (int i = 0; i < 9; i++)
                if (i != c && txt[r, i].Text == num.ToString())
                    return false;

            for (int i = 0; i < 9; i++)
                if (i != r && txt[i, c].Text == num.ToString())
                    return false;

            int sr = r / 3 * 3;
            int sc = c / 3 * 3;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    int x = sr + i;
                    int y = sc + j;

                    if ((x != r || y != c) && txt[x, y].Text == num.ToString())
                        return false;
                }

            return true;
        }

        bool KiemTraHoanThanh()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (txt[i, j].Text == "")
                        return false;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    int val = int.Parse(txt[i, j].Text);
                    if (!HopLeCheck(i, j, val))
                        return false;
                }

            return true;
        }

        // ================= BUTTON =================
        void btnNew_Click(object sender, EventArgs e)
        {
            level = cboLevel.SelectedIndex + 1;
            TaoMoiGame();
        }

        void btnGiai_Click(object sender, EventArgs e)
        {
            Solve(0, 0);
            HienThi();
        }
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}