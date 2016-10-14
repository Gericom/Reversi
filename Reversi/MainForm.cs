using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var r = new ReversiGame(6);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm().ShowDialog();
        }
    }
}
