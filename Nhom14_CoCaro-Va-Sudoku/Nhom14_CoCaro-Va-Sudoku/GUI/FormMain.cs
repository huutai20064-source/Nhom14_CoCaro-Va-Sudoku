using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public partial class FormMain : Form
    {
        UserControl currentGame;

        public FormMain()
        {
            InitializeComponent();

            this.BackgroundImage = Properties.Resources.CaroAndSudoku;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
            this.Paint += VeOverlay;

            StyleButton(btnCaro, Color.FromArgb(52, 152, 219));
            StyleButton(btnSudoku, Color.FromArgb(231, 76, 60));

            this.Resize += (s, e) => UpdateLayout();

            // 👉 FIX LỆCH LẦN ĐẦU
            this.Load += (s, e) => UpdateLayout();
        }

        // ================= BACKGROUND OVERLAY =================
        void VeOverlay(object sender, PaintEventArgs e)
        {
            using (Brush b = new SolidBrush(Color.FromArgb(120, 0, 0, 0)))
            {
                e.Graphics.FillRectangle(b, this.ClientRectangle);
            }
        }

        // ================= STYLE BUTTON =================
        void StyleButton(Button b, Color color)
        {
            b.Width = 220;
            b.Height = 50;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 12, FontStyle.Bold);
        }

        // ================= CARO =================
        private void BtnCaro_Click(object sender, EventArgs e)
        {
            FormChonCheDo f = new FormChonCheDo();

            if (f.ShowDialog() != DialogResult.OK)
                return;

            if (f.CheDo == 0)
                HienGame(new CaroControl(false));
            else
                HienGame(new CaroControl(true, f.DoKho));
        }

        // ================= SUDOKU =================
        private void BtnSudoku_Click(object sender, EventArgs e)
        {
            HienGame(new SudokuControl());
        }

        // ================= HIỂN THỊ GAME =================
        void HienGame(UserControl game)
        {
            pnlContainer.Controls.Clear();

            currentGame = game;

            game.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(game);

            pnlContainer.Controls.Add(btnHome);
            btnHome.BringToFront();

            pnlContainer.Visible = true;

            lblTitle.Visible = false;
            btnCaro.Visible = false;
            btnSudoku.Visible = false;
        }

        // ================= HOME =================
        private void BtnHome_Click(object sender, EventArgs e)
        {
            pnlContainer.Controls.Clear();

            if (currentGame != null)
            {
                currentGame.Dispose();
                currentGame = null;
            }

            pnlContainer.Visible = false;

            lblTitle.Visible = true;
            btnCaro.Visible = true;
            btnSudoku.Visible = true;

            UpdateLayout();
        }

        // ================= RESPONSIVE =================
        void UpdateLayout()
        {
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;

            if (lblTitle.Visible)
            {
                int tongChieuCao =
                    lblTitle.Height +
                    250 +
                    btnCaro.Height +
                    10 +
                    btnSudoku.Height;

                int startY = (h - tongChieuCao) / 2;

                lblTitle.Left = (w - lblTitle.Width) / 2;
                lblTitle.Top = startY;

                btnCaro.Left = (w - btnCaro.Width) / 2;
                btnCaro.Top = lblTitle.Bottom + 20;

                btnSudoku.Left = (w - btnSudoku.Width) / 2;
                btnSudoku.Top = btnCaro.Bottom + 10;
            }

            pnlContainer.Left = 0;
            pnlContainer.Top = 0;
            pnlContainer.Width = w;
            pnlContainer.Height = h;
        }
    }
}