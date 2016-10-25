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
        private int mBoardSize = 6;

        private bool mGameEnded = false;

        public MainForm()
        {
            InitializeComponent();
            Microsoft.Win32.RegistryKey reversi = 
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Florian and Ruben").CreateSubKey("Reversi");
            if (!reversi.GetValueNames().Contains("BoardSize"))
                reversi.SetValue("BoardSize", 6);
            else
                mBoardSize = (int)reversi.GetValue("BoardSize");
            mBoardSize = (mBoardSize + 1) & ~1;
            if (mBoardSize < 6)
                mBoardSize = 6;
            if (mBoardSize > 12)
                mBoardSize = 12;
            reversi.SetValue("BoardSize", mBoardSize);
            reversi.Close();
            reversiBoard2.Game = new ReversiGame(mBoardSize);
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void optionsMenuItem_Click(object sender, EventArgs e)
        {
            var o = new OptionsForm(mBoardSize);
            o.ShowDialog();
            Microsoft.Win32.RegistryKey reversi = 
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Florian and Ruben").CreateSubKey("Reversi");
            bool different = mBoardSize != o.BoardSize;
            mBoardSize = o.BoardSize;
            reversi.SetValue("BoardSize", o.BoardSize);
            reversi.Close();
            if (different)
                MessageBox.Show("Start a new game to use the new settings", "Settings Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

		private void reversiBoard2_GameEnd()
		{
            mGameEnded = true;
            int player1count = reversiBoard2.Game.GetNrStonesForPlayer(ReversiGame.ReversiField.FieldContent.Player1);
            int player2count = reversiBoard2.Game.GetNrStonesForPlayer(ReversiGame.ReversiField.FieldContent.Player2);
            VictoryForm.GameResult result;
            if (player1count > player2count)
            {
                result = VictoryForm.GameResult.Player1Won;
                toolStripStatusLabel3.Text = "Player 1 Won!";
            }
            else if (player1count < player2count)
            {
                result = VictoryForm.GameResult.Player2Won;
                toolStripStatusLabel3.Text = "Player 2 Won!";
            }
            else
            {
                result = VictoryForm.GameResult.Tie;
                toolStripStatusLabel3.Text = "It's a Tie!";
            }
            toolStripStatusLabel1.Text = "Player 1: " + player1count;
            toolStripStatusLabel2.Text = "Player 2: " + player2count;
            new VictoryForm(result, player1count, player2count).ShowDialog();
		}

		private void reversiBoard2_PassRequired()
		{
            if (mGameEnded)
                return;
            if (MessageBox.Show("You can't do a single move, you need to pass 😋", "Passing", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
			    reversiBoard2.PassTurn();
		}

		private void reversiBoard2_PlayerSwitch(ReversiBoard.Turn newPlayer)
		{
            if (mGameEnded)
                return;

            if ( newPlayer == ReversiBoard.Turn.Player1 )
				toolStripStatusLabel3.Text = "Player 1's Turn";
			else
				toolStripStatusLabel3.Text = "Player 2's Turn";

			toolStripStatusLabel1.Text = "Player 1: " + reversiBoard2.Game.GetNrStonesForPlayer(ReversiGame.ReversiField.FieldContent.Player1);
			toolStripStatusLabel2.Text = "Player 2: " + reversiBoard2.Game.GetNrStonesForPlayer(ReversiGame.ReversiField.FieldContent.Player2);
		}

        private void newGameMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start a new game?", "New Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                toolStripStatusLabel1.Text = "Player 1: 2";
                toolStripStatusLabel2.Text = "Player 2: 2";
                toolStripStatusLabel3.Text = "Player 1's Turn";
                mGameEnded = false;
                reversiBoard2.Game = new ReversiGame(mBoardSize);
                reversiBoard2.Invalidate();
            }
        }
    }
}
