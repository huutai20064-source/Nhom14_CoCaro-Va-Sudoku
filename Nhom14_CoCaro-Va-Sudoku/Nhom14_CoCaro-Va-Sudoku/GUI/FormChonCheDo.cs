using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public class FormChonCheDo : Form
    {
        Panel card;
        Label lblTitle;
        Button btnPVP, btnAI;
        ComboBox cboDoKho;

        public int CheDo = -1;
        public int DoKho = 1;
        public bool DaChon = false;

        public FormChonCheDo()
        {
            this.Text = "Chọn chế độ";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(30, 30, 40);

            TaoUI();

            this.Resize += (s, e) => CanGiua();
            this.FormClosing += FormChonCheDo_FormClosing;
        }

        void TaoUI()
        {
            card = new Panel();
            card.Size = new Size(320, 260);
            card.BackColor = Color.White;
            Controls.Add(card);

            lblTitle = new Label();
            lblTitle.Text = "CHỌN CHẾ ĐỘ";
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(40, 40, 60);
            lblTitle.AutoSize = true;
            card.Controls.Add(lblTitle);

            btnPVP = new Button();
            btnPVP.Text = "👥 2 Người chơi";
            StyleButton(btnPVP, Color.FromArgb(52, 152, 219));
            btnPVP.Click += (s, e) =>
            {
                CheDo = 0;
                DaChon = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            card.Controls.Add(btnPVP);

            cboDoKho = new ComboBox();
            cboDoKho.Items.AddRange(new string[] { "Dễ", "Trung bình", "Khó" });
            cboDoKho.SelectedIndex = 0;
            cboDoKho.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDoKho.Font = new Font("Segoe UI", 11);
            card.Controls.Add(cboDoKho);

            btnAI = new Button();
            btnAI.Text = "🤖 Chơi với máy";
            StyleButton(btnAI, Color.FromArgb(231, 76, 60));
            btnAI.Click += (s, e) =>
            {
                CheDo = 1;
                DoKho = cboDoKho.SelectedIndex + 1;
                DaChon = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            card.Controls.Add(btnAI);

            CanGiua();
        }

        void StyleButton(Button b, Color color)
        {
            b.Width = 220;
            b.Height = 45;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        }

        void CanGiua()
        {
            card.Left = (this.ClientSize.Width - card.Width) / 2;
            card.Top = (this.ClientSize.Height - card.Height) / 2;

            lblTitle.Left = (card.Width - lblTitle.Width) / 2;
            lblTitle.Top = 20;

            btnPVP.Left = (card.Width - btnPVP.Width) / 2;
            btnPVP.Top = lblTitle.Bottom + 20;

            cboDoKho.Width = 220;
            cboDoKho.Left = (card.Width - cboDoKho.Width) / 2;
            cboDoKho.Top = btnPVP.Bottom + 15;

            btnAI.Left = (card.Width - btnAI.Width) / 2;
            btnAI.Top = cboDoKho.Bottom + 15;
        }

        void FormChonCheDo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!DaChon)
            {
                CheDo = -1; // bấm X
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}