using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    partial class FormMain
    {
        private Label lblTitle;
        private Button btnCaro;
        private Button btnSudoku;
        private Panel pnlContainer;
        private Button btnHome;

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCaro = new System.Windows.Forms.Button();
            this.btnSudoku = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(140, 60);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(297, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "GAME CENTER";
            // 
            // btnCaro
            // 
            this.btnCaro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCaro.Location = new System.Drawing.Point(150, 180);
            this.btnCaro.Name = "btnCaro";
            this.btnCaro.Size = new System.Drawing.Size(220, 70);
            this.btnCaro.TabIndex = 1;
            this.btnCaro.Text = "CỜ CARO";
            this.btnCaro.Click += new System.EventHandler(this.BtnCaro_Click);
            // 
            // btnSudoku
            // 
            this.btnSudoku.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSudoku.Location = new System.Drawing.Point(150, 270);
            this.btnSudoku.Name = "btnSudoku";
            this.btnSudoku.Size = new System.Drawing.Size(220, 70);
            this.btnSudoku.TabIndex = 2;
            this.btnSudoku.Text = "SUDOKU";
            this.btnSudoku.Click += new System.EventHandler(this.BtnSudoku_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.Controls.Add(this.btnHome);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(520, 700);
            this.pnlContainer.TabIndex = 3;
            this.pnlContainer.Visible = false;
            // 
            // btnHome
            // 
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHome.Location = new System.Drawing.Point(10, 10);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(90, 35);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "⬅ Home";
            this.btnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(520, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCaro);
            this.Controls.Add(this.btnSudoku);
            this.Controls.Add(this.pnlContainer);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Center";
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}