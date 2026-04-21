using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        // ================= OPEN CARO =================
        private void BtnCaro_Click(object sender, EventArgs e)
        {
            FormCaro f = new FormCaro();
            f.Show();
        }

        // ================= OPEN SUDOKU =================
        private void BtnSudoku_Click(object sender, EventArgs e)
        {
            FormSudoku f = new FormSudoku();
            f.Show();
        }

        // ================= OPTIONAL: EFFECT (NHẸ) =================
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Opacity = 0;
            Timer t = new Timer();
            t.Interval = 10;

            t.Tick += (s, ev) =>
            {
                this.Opacity += 0.05;
                if (this.Opacity >= 1)
                    t.Stop();
            };

            t.Start();
        }
    }
}