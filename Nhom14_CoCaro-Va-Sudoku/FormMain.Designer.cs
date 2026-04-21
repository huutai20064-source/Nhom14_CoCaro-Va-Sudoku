namespace Nhom14_CoCaro_Va_Sudoku
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCaro;
        private System.Windows.Forms.Button btnSudoku;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCaro = new System.Windows.Forms.Button();
            this.btnSudoku = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // ================= FORM =================
            this.ClientSize = new System.Drawing.Size(520, 380);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Center - Nhom14";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(235, 240, 245);

            // ================= TITLE =================
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(135, 40);
            this.lblTitle.Text = "GAME CENTER";
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);

            // ================= CARO BUTTON =================
            this.btnCaro.Location = new System.Drawing.Point(160, 130);
            this.btnCaro.Size = new System.Drawing.Size(200, 60);
            this.btnCaro.Text = "🎯 CỜ CARO";
            this.btnCaro.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnCaro.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnCaro.ForeColor = System.Drawing.Color.White;
            this.btnCaro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaro.FlatAppearance.BorderSize = 0;
            this.btnCaro.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnCaro.Click += new System.EventHandler(this.BtnCaro_Click);

            this.btnCaro.MouseEnter += (s, e) =>
            {
                btnCaro.BackColor = System.Drawing.Color.FromArgb(0, 150, 255);
            };

            this.btnCaro.MouseLeave += (s, e) =>
            {
                btnCaro.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            };

            // ================= SUDOKU BUTTON =================
            this.btnSudoku.Location = new System.Drawing.Point(160, 220);
            this.btnSudoku.Size = new System.Drawing.Size(200, 60);
            this.btnSudoku.Text = "🧩 SUDOKU";
            this.btnSudoku.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSudoku.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnSudoku.ForeColor = System.Drawing.Color.White;
            this.btnSudoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSudoku.FlatAppearance.BorderSize = 0;
            this.btnSudoku.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnSudoku.Click += new System.EventHandler(this.BtnSudoku_Click);

            this.btnSudoku.MouseEnter += (s, e) =>
            {
                btnSudoku.BackColor = System.Drawing.Color.FromArgb(80, 220, 140);
            };

            this.btnSudoku.MouseLeave += (s, e) =>
            {
                btnSudoku.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            };

            // ================= ADD =================
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCaro);
            this.Controls.Add(this.btnSudoku);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}