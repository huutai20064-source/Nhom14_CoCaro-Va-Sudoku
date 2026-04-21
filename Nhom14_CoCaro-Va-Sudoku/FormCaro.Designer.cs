using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    partial class FormCaro
    {
        Panel pnlBanCo;
        Label lblTurn;
        Button btnReset;
        Label lblTitle;

        void InitializeComponent()
        {
            this.pnlBanCo = new Panel();
            this.lblTurn = new Label();
            this.btnReset = new Button();
            this.lblTitle = new Label();
            this.SuspendLayout();

            // ===== TITLE =====
            this.lblTitle.Text = "GAME CỜ CARO";
            this.lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(150, 10);

            // ===== PANEL BÀN CỜ =====
            this.pnlBanCo.Location = new Point(20, 50);
            this.pnlBanCo.Size = new Size(460, 460);
            this.pnlBanCo.BorderStyle = BorderStyle.FixedSingle;
            this.pnlBanCo.BackColor = Color.White;

            // ===== LABEL LƯỢT =====
            this.lblTurn.Location = new Point(20, 520);
            this.lblTurn.Size = new Size(200, 30);
            this.lblTurn.Font = new Font("Arial", 10, FontStyle.Bold);
            this.lblTurn.Text = "Lượt: X";

            // ===== BUTTON RESET =====
            this.btnReset.Location = new Point(350, 515);
            this.btnReset.Size = new Size(120, 35);
            this.btnReset.Text = "Chơi lại";
            this.btnReset.BackColor = Color.LightGray;
            this.btnReset.Click += (s, e) => { ResetGame(); };

            // ===== FORM =====
            this.ClientSize = new Size(500, 570);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlBanCo);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.btnReset);
            this.Text = "Cờ Caro";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}