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
            this.lblTitle.Font = new Font("Verdana", 26F, FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(130, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(327, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "GAME CENTER";
            this.lblTitle.ForeColor = Color.FromArgb(255, 215, 0);
            this.lblTitle.BackColor = Color.Transparent;


            // 
            // btnCaro
            // 
            this.btnCaro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnCaro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaro.FlatAppearance.BorderSize = 0;
            this.btnCaro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaro.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCaro.ForeColor = System.Drawing.Color.White;
            this.btnCaro.Location = new System.Drawing.Point(130, 200);
            this.btnCaro.Name = "btnCaro";
            this.btnCaro.Size = new System.Drawing.Size(260, 70);
            this.btnCaro.TabIndex = 1;
            this.btnCaro.Text = "🎮 CỜ CARO";
            this.btnCaro.UseVisualStyleBackColor = false;
            this.btnCaro.Click += new System.EventHandler(this.BtnCaro_Click);
            // 
            // btnSudoku
            // 
            this.btnSudoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSudoku.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSudoku.FlatAppearance.BorderSize = 0;
            this.btnSudoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSudoku.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSudoku.ForeColor = System.Drawing.Color.White;
            this.btnSudoku.Location = new System.Drawing.Point(130, 290);
            this.btnSudoku.Name = "btnSudoku";
            this.btnSudoku.Size = new System.Drawing.Size(260, 70);
            this.btnSudoku.TabIndex = 2;
            this.btnSudoku.Text = "🧩 SUDOKU";
            this.btnSudoku.UseVisualStyleBackColor = false;
            this.btnSudoku.Click += new System.EventHandler(this.BtnSudoku_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.BackColor = System.Drawing.Color.White;
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
            this.btnHome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Location = new System.Drawing.Point(15, 10);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(100, 35);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "⬅ Home";
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.BtnHome_Click);
            // 
            // FormMain
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(520, 700);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCaro);
            this.Controls.Add(this.btnSudoku);
            this.Controls.Add(this.pnlContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Center";
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}