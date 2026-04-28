using System;
using System.Windows.Forms;

namespace Nhom14_CoCaro_Va_Sudoku
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void BtnCaro_Click(object sender, EventArgs e)
        {
            HienGame(new CaroControl());
        }

        private void BtnSudoku_Click(object sender, EventArgs e)
        {
            HienGame(new SudokuControl());
        }

        void HienGame(UserControl game)
        {
            pnlContainer.Controls.Clear();

            game.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(game);

            pnlContainer.Controls.Add(btnHome);
            btnHome.BringToFront();

            pnlContainer.Visible = true;

            lblTitle.Visible = false;
            btnCaro.Visible = false;
            btnSudoku.Visible = false;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            pnlContainer.Visible = false;

            lblTitle.Visible = true;
            btnCaro.Visible = true;
            btnSudoku.Visible = true;
        }
    }
}