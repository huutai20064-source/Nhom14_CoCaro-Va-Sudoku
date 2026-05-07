using System;
using System.Drawing;
using System.Windows.Forms;
using Nhom14_CoCaro_Va_Sudoku.BUS;
using Nhom14_CoCaro_Va_Sudoku.DTO;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public class SudokuControl : UserControl
    {
        TextBox[,] txt = new TextBox[9, 9];
        Button[] btnSo = new Button[9];

        int selectedRow = -1;
        int selectedCol = -1;
        int soDangChon = 0;
        int level = 1;

        SudokuEngine engine = new SudokuEngine();
        SudokuBUS bus = new SudokuBUS();

        int nguoiChoiId;

        Label lblTitle, lblTime;
        ComboBox cboLevel;
        Button btnNew, btnGiai;

        Panel pnlBoard;

        int startY = 100;
        int cell;
        int gridSize;

        Timer timer;
        DateTime startTime;

        public SudokuControl()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.SizeChanged += (s, e) => UpdateLayout();

            TaoUI();
            TaoLuoi();
            TaoBangSo();

            nguoiChoiId = bus.LayNguoiChoi();

            TaoGame();
            StartTimer();
        }

        void TaoUI()
        {
            lblTitle = new Label();
            lblTitle.Text = "SUDOKU";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.AutoSize = true;
            Controls.Add(lblTitle);

            lblTime = new Label();
            lblTime.Text = "00:00";
            lblTime.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblTime.ForeColor = Color.White;
            lblTime.AutoSize = true;
            Controls.Add(lblTime);

            cboLevel = new ComboBox();
            cboLevel.Items.AddRange(new string[] { "Dễ", "Trung bình", "Khó" });
            cboLevel.SelectedIndex = 0;
            cboLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLevel.BackColor = Color.FromArgb(30, 30, 30);
            cboLevel.ForeColor = Color.White;
            cboLevel.SelectedIndexChanged += (s, e) =>
            {
                level = cboLevel.SelectedIndex + 1;
                TaoGame();
            };
            Controls.Add(cboLevel);

            btnNew = new Button();
            btnNew.Text = "TẠO MỚI";
            StyleButton(btnNew, Color.FromArgb(0, 120, 215));
            btnNew.Click += (s, e) =>
            {
                if (MessageBox.Show("Tạo ván mới?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    TaoGame();
            };
            btnNew.AutoSize = true;

            Controls.Add(btnNew);

            btnGiai = new Button();
            btnGiai.Text = "GIẢI";
            StyleButton(btnGiai, Color.FromArgb(200, 60, 60));
            btnGiai.Click += BtnGiai_Click;
            btnGiai.AutoSize = true;

            Controls.Add(btnGiai);

            pnlBoard = new Panel();
            pnlBoard.BorderStyle = BorderStyle.FixedSingle;
            pnlBoard.BackColor = Color.Black;
            Controls.Add(pnlBoard);

            this.Click += BoChon;
            pnlBoard.Click += BoChon;
        }
        void BoChon(object sender, EventArgs e)
        {
            selectedRow = -1;
            selectedCol = -1;
            VeLaiMau();
        }
        void StyleButton(Button btn, Color color)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        void TaoLuoi()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j] = new TextBox();
                    txt[i, j].BorderStyle = BorderStyle.FixedSingle;
                    txt[i, j].Multiline = true;
                    txt[i, j].TextAlign = HorizontalAlignment.Center;
                    txt[i, j].Font = new Font("Segoe UI", 18, FontStyle.Bold);
                    txt[i, j].MaxLength = 1;

                    int r = i, c = j;

                    txt[i, j].Click += (s, e) =>
                    {
                        selectedRow = r;
                        selectedCol = c;
                        VeLaiMau();
                    };

                    txt[i, j].KeyPress += NhapSo;

                    pnlBoard.Controls.Add(txt[i, j]);
                }
        }

        void TaoBangSo()
        {
            for (int i = 0; i < 9; i++)
            {
                btnSo[i] = new Button();
                btnSo[i].Text = (i + 1).ToString();
                btnSo[i].Size = new Size(40, 40);

                StyleButton(btnSo[i], Color.FromArgb(60, 60, 60));

                int so = i + 1;

                btnSo[i].Click += (s, e) =>
                {
                    if (soDangChon == so)
                        soDangChon = 0;
                    else
                        soDangChon = so;

                    VeLaiMau();
                };

                Controls.Add(btnSo[i]);
            }
        }

        void TaoGame()
        {
            engine.TaoGame(level);
            StartTimer();

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j].Text = engine.board[i, j] == 0 ? "" : engine.board[i, j].ToString();
                    txt[i, j].Enabled = engine.board[i, j] == 0;
                    txt[i, j].BackColor = Color.White;
                }

            selectedRow = -1;
            selectedCol = -1;
            soDangChon = 0;

            VeLaiMau();
            UpdateLayout();
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

            VeLaiMau();

            if (KiemTraHoanThanh())
            {
                timer.Stop();

                int tg = (int)(DateTime.Now - startTime).TotalSeconds;

                MessageBox.Show("Hoàn thành!");

                SudokuDTO dto = new SudokuDTO
                {
                    NguoiChoiId = nguoiChoiId,
                    DoKho = cboLevel.Text,
                    ThoiGianChoi = tg,
                    KetQua = "Win",
                    NgayChoi = DateTime.Now
                };

                bus.Thang(dto);
            }
        }

        bool KiemTraHoanThanh()
        {
            int[,] user = new int[9, 9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    user[i, j] = txt[i, j].Text == "" ? 0 : int.Parse(txt[i, j].Text);

            return engine.KiemTraDayDuHopLe(user);
        }

        void BtnGiai_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xem lời giải?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            timer.Stop();

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j].Text = engine.solution[i, j].ToString();
                    txt[i, j].Enabled = false;
                }

            MessageBox.Show("Bạn đã thua!");

            SudokuDTO dto = new SudokuDTO
            {
                NguoiChoiId = nguoiChoiId,
                DoKho = cboLevel.Text,
                ThoiGianChoi = 0,
                KetQua = "Lose",
                NgayChoi = DateTime.Now
            };

            bus.Thua(dto);
        }

        void StartTimer()
        {
            if (timer != null) timer.Stop();

            startTime = DateTime.Now;

            timer = new Timer();
            timer.Interval = 1000;

            timer.Tick += (s, e) =>
            {
                int t = (int)(DateTime.Now - startTime).TotalSeconds;
                lblTime.Text = TimeSpan.FromSeconds(t).ToString(@"mm\:ss");
            };

            timer.Start();
        }

        void VeLaiMau()
        {
            ResetMau();
            Highlight();
            ToSangSo();
            DanhDauLoi();
        }

        void ResetMau()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    txt[i, j].BackColor =
                        (i / 3 + j / 3) % 2 == 0
                        ? Color.White
                        : Color.LightBlue;
        }

        void Highlight()
        {
            if (selectedRow < 0 || selectedCol < 0) return;

            for (int i = 0; i < 9; i++)
            {
                txt[selectedRow, i].BackColor = Color.PaleGreen;
                txt[i, selectedCol].BackColor = Color.PaleGreen;
            }

            txt[selectedRow, selectedCol].BackColor = Color.DeepSkyBlue;
        }

        void ToSangSo()
        {
            if (soDangChon == 0) return;

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (txt[i, j].Text == soDangChon.ToString())
                        txt[i, j].BackColor = Color.Gold;
        }

        void DanhDauLoi()
        {
            int[,] user = new int[9, 9];

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    user[i, j] = txt[i, j].Text == "" ? 0 : int.Parse(txt[i, j].Text);

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (txt[i, j].Text != "")
                        if (!engine.KiemTraHopLeUser(user, i, j, user[i, j]))
                            txt[i, j].BackColor = Color.LightCoral;
        }

        void UpdateLayout()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            gridSize = Math.Min(w - 40, h - 260);
            cell = gridSize / 9;

            int centerX = w / 2;

            lblTitle.Location = new Point(centerX - lblTitle.Width / 2, 10);
            lblTime.Location = new Point(centerX - lblTime.Width / 2, 45);

            pnlBoard.Size = new Size(gridSize, gridSize);
            pnlBoard.Location = new Point(centerX - gridSize / 2, startY);

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    txt[i, j].Size = new Size(cell - 2, cell - 2);
                    txt[i, j].Location = new Point(j * cell, i * cell);
                }

            int bottom = pnlBoard.Bottom;
            int startBtnX = centerX - 200;

            for (int i = 0; i < 9; i++)
                btnSo[i].Location = new Point(startBtnX + i * 45, bottom + 10);

            cboLevel.Location = new Point(centerX - 60, bottom + 60);
            btnNew.Location = new Point(centerX - 120, bottom + 100);
            btnGiai.Location = new Point(centerX + 20, bottom + 100);

            Invalidate();
        }

        void VeKhung(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Pen penNho = new Pen(Color.Gray, 1);
            Pen penLon = new Pen(Color.Black, 3);

            for (int i = 0; i <= 9; i++)
            {
                int x = pnlBoard.Left + i * cell;
                int y = pnlBoard.Top + i * cell;

                g.DrawLine(i % 3 == 0 ? penLon : penNho, x, pnlBoard.Top, x, pnlBoard.Bottom);
                g.DrawLine(i % 3 == 0 ? penLon : penNho, pnlBoard.Left, y, pnlBoard.Right, y);
            }
        }
    }
}