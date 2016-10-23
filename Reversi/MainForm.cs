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
            reversiBoard2.Game = r;
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsMenuItem_Click(object sender, EventArgs e)
        {
            new OptionsForm().ShowDialog();
        }

		private void reversiBoard2_PlayerSwitch(ReversiBoard.Turn newPlayer)
		{
			if ( newPlayer == ReversiBoard.Turn.Player1 )
				toolStripStatusLabel3.Text = "Player 1's Turn";
			else
				toolStripStatusLabel3.Text = "Player 2's Turn";

			toolStripStatusLabel1.Text = "Player 1: " + reversiBoard2.Game.GetNrStonesForPlayer(ReversiGame.ReversiField.FieldContent.Player1);
			toolStripStatusLabel2.Text = "Player 2: " + reversiBoard2.Game.GetNrStonesForPlayer(ReversiGame.ReversiField.FieldContent.Player2);
		}
	}
}
