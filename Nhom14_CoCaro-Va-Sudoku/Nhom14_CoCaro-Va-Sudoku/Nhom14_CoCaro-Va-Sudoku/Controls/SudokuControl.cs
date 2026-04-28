using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public class SudokuControl : UserControl
    {
        TextBox[,] txt = new TextBox[9, 9];
        int[,] board = new int[9, 9];
        int[,] solution = new int[9, 9];

        Button[] btnSo = new Button[9];
        int soDangChon = 0;

        Random rand = new Random();
        int level = 1;

        bool daThongBao = false;

        Button btnNew, btnGiai;
        ComboBox cboLevel;

        // ===== SQL =====
        SqlConnection conn;
        int nguoiChoiId = 1;
        DateTime startTime;

        public SudokuControl()
        {
            this.Size = new Size(520, 750);

            TaoUI();
            TaoLuoi();
            TaoBangSo();
            ResetMau();
            KetNoi();
            LayNguoiChoi();

            TaoMoiGame();
        }

        // ===== SQL =====
        void KetNoi()
        {
            conn = new SqlConnection("Data Source=.;Initial Catalog=GameCenter;Integrated Security=True;");
        }

        void LayNguoiChoi()
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id FROM NguoiChoi ORDER BY Id ASC", conn);
            object kq = cmd.ExecuteScalar();

            if (kq != null)
                nguoiChoiId = Convert.ToInt32(kq);

            conn.Close();
        }

        void LuuTran(string ketQua, int thoiGian)
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO TranSudoku(NguoiChoiId, DoKho, ThoiGianChoi, KetQua) VALUES (@id,@dk,@tg,@kq)", conn);

            cmd.Parameters.AddWithValue("@id", nguoiChoiId);
            cmd.Parameters.AddWithValue("@dk", cboLevel.Text);
            cmd.Parameters.AddWithValue("@tg", thoiGian);
            cmd.Parameters.AddWithValue("@kq", ketQua);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        void CapNhatDiem(bool thang)
        {
            conn.Open();

            SqlCommand cmd;

            if (thang)
            {
                cmd = new SqlCommand(
                    "UPDATE NguoiChoi SET DiemSudoku = DiemSudoku + 10, SoTranThang = SoTranThang + 1 WHERE Id=@id", conn);
            }
            else
            {
                cmd = new SqlCommand(
                    "UPDATE NguoiChoi SET SoTranThua = SoTranThua + 1 WHERE Id=@id", conn);
            }

            cmd.Parameters.AddWithValue("@id", nguoiChoiId);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        // ===== UI =====
        void TaoUI()
        {
            Label lblTitle = new Label();
            lblTitle.Text = "SUDOKU";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(210, 10);
            Controls.Add(lblTitle);

            cboLevel = new ComboBox();
            cboLevel.Items.AddRange(new string[] { "Dễ", "Trung bình", "Khó" });
            cboLevel.SelectedIndex = 0;
            cboLevel.Size = new Size(120, 30);
            cboLevel.Location = new Point(200, 560);
            cboLevel.SelectedIndexChanged += (s, e) =>
            {
                level = cboLevel.SelectedIndex + 1;
                TaoMoiGame();
            };
            Controls.Add(cboLevel);

            btnGiai = new Button();
            btnGiai.Text = "Giải";
            btnGiai.Size = new Size(110, 40);
            btnGiai.Location = new Point(110, 600);
            btnGiai.BackColor = Color.LightGreen;
            btnGiai.Click += BtnGiai_Click;
            Controls.Add(btnGiai);

            btnNew = new Button();
            btnNew.Text = "Tạo mới";
            btnNew.Size = new Size(110, 40);
            btnNew.Location = new Point(280, 600);
            btnNew.BackColor = Color.LightGray;
            btnNew.Click += (s, e) => TaoMoiGame();
            Controls.Add(btnNew);
        }

        // ===== GRID =====
        void TaoLuoi()
        {
            int gridSize = 450;
            int cell = gridSize / 9;

            int startX = (this.Width - gridSize) / 2;
            int startY = 60;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j] = new TextBox();

                    txt[i, j].Size = new Size(cell, cell);
                    txt[i, j].Location = new Point(startX + j * cell, startY + i * cell);

                    txt[i, j].BorderStyle = BorderStyle.FixedSingle;
                    txt[i, j].TextAlign = HorizontalAlignment.Center;
                    txt[i, j].Font = new Font("Segoe UI", 18, FontStyle.Bold);

                    txt[i, j].MaxLength = 1;
                    txt[i, j].ShortcutsEnabled = false;

                    txt[i, j].KeyPress += NhapSo;

                    Controls.Add(txt[i, j]);
                }
        }

        // ===== NÚT 1-9 =====
        void TaoBangSo()
        {
            int startX = (this.Width - (9 * 45)) / 2;
            int y = 510;

            for (int i = 0; i < 9; i++)
            {
                btnSo[i] = new Button();
                btnSo[i].Text = (i + 1).ToString();
                btnSo[i].Size = new Size(42, 42);
                btnSo[i].Location = new Point(startX + i * 45, y);

                int so = i + 1;
                btnSo[i].Click += (s, e) =>
                {
                    soDangChon = so;
                    ToSangSo();
                };

                Controls.Add(btnSo[i]);
            }
        }

        void ToSangSo()
        {
            ResetMau();

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (txt[i, j].Text != "" &&
                        int.Parse(txt[i, j].Text) == soDangChon)
                        txt[i, j].BackColor = Color.Gold;
        }

        // ===== GAME =====
        void TaoMoiGame()
        {
            board = new int[9, 9];
            daThongBao = false;

            startTime = DateTime.Now; // SQL TIME

            Solve(0, 0);

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = board[i, j];

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

            foreach (int num in nums)
            {
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
                    }
                    else txt[i, j].Text = "";
                }
        }

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
                e.Handled = true;
            }

            ResetMau();
            DanhDauLoi();

            if (!daThongBao && KiemTraHoanThanh())
            {
                daThongBao = true;

                int tg = (int)(DateTime.Now - startTime).TotalSeconds;

                MessageBox.Show("🎉 Hoàn thành!");

                LuuTran("Win", tg);
                CapNhatDiem(true);
            }
        }

        void DanhDauLoi()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (txt[i, j].Text == "") continue;

                    int val = int.Parse(txt[i, j].Text);

                    if (!HopLeCheck(i, j, val))
                        txt[i, j].BackColor = Color.LightCoral;
                }
        }

        void ResetMau()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    txt[i, j].BackColor =
                        (i / 3 + j / 3) % 2 == 0
                        ? Color.FromArgb(235, 245, 255)
                        : Color.White;
        }

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
                    if (txt[i, j].Text != solution[i, j].ToString())
                        return false;

            return true;
        }

        void BtnGiai_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j].Text = solution[i, j].ToString();
                    txt[i, j].Enabled = false;
                }

            LuuTran("Lose", 999);
            CapNhatDiem(false);
        }
    }
}