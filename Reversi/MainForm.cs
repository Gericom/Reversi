using Microsoft.Win32;
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
            //Open or create the registry key containing the board size
            RegistryKey reversi = 
                Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Florian and Ruben").CreateSubKey("Reversi");
            //if it doesn't exist already, set the default value
            if (!reversi.GetValueNames().Contains("BoardSize"))
                reversi.SetValue("BoardSize", 6);
            else
                mBoardSize = (int)reversi.GetValue("BoardSize");
            //multiple of 2
            mBoardSize = (mBoardSize + 1) & ~1;
            //min 6 max 12
            if (mBoardSize < 6)
                mBoardSize = 6;
            if (mBoardSize > 12)
                mBoardSize = 12;
            //store fixed value in registry
            reversi.SetValue("BoardSize", mBoardSize);
            //close the registry key
            reversi.Close();
            //new game
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
            if (mBoardSize != o.BoardSize)
            {
                //store the new board size value in the registry
                RegistryKey reversi =
                    Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Florian and Ruben").CreateSubKey("Reversi");
                mBoardSize = o.BoardSize;
                reversi.SetValue("BoardSize", o.BoardSize);
                reversi.Close();
                MessageBox.Show("Start a new game to use the new settings", "Settings Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //When the game has ended, show the victory screen (with the music (; )
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

        //When a pass is required notify the user about it
		private void reversiBoard2_PassRequired()
		{
            if (mGameEnded)
                return;
            if (MessageBox.Show("You can't do a single move, you need to pass 😋", "Passing", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
			    reversiBoard2.PassTurn();
		}

        //Update the status label when the players switch
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
