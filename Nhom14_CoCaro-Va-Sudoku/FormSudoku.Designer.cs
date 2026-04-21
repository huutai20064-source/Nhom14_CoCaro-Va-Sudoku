using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    partial class FormSudoku
    {
        Button btnGiai;
        Button btnNew;
        ComboBox cboLevel;
        Label lblTitle;

        void InitializeComponent()
        {
            this.btnGiai = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cboLevel = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGiai
            // 
            this.btnGiai.BackColor = System.Drawing.Color.LightGreen;
            this.btnGiai.Location = new System.Drawing.Point(118, 603);
            this.btnGiai.Name = "btnGiai";
            this.btnGiai.Size = new System.Drawing.Size(100, 48);
            this.btnGiai.TabIndex = 2;
            this.btnGiai.Text = "Giải";
            this.btnGiai.UseVisualStyleBackColor = false;
            this.btnGiai.Click += new System.EventHandler(this.btnGiai_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.LightGray;
            this.btnNew.Location = new System.Drawing.Point(258, 603);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 48);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "Tạo mới";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cboLevel
            // 
            this.cboLevel.Items.AddRange(new object[] {
            "Dễ",
            "Trung bình",
            "Khó"});
            this.cboLevel.Location = new System.Drawing.Point(175, 573);
            this.cboLevel.Name = "cboLevel";
            this.cboLevel.Size = new System.Drawing.Size(120, 24);
            this.cboLevel.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(120, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(238, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SUDOKU GAME";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // FormSudoku
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(493, 694);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cboLevel);
            this.Controls.Add(this.btnGiai);
            this.Controls.Add(this.btnNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSudoku";
            this.Text = "Sudoku";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}