using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class VictoryForm : Form
    {
        public enum GameResult
        {
            Player1Won,
            Player2Won,
            Tie
        }

        SoundPlayer mSoundPlayer;

        public VictoryForm(GameResult result, int score1, int score2)
        {
            InitializeComponent();
            if (result == GameResult.Player1Won)
                label1.Text = "Player 1 Won!";
            else if(result == GameResult.Player2Won)
                label1.Text = "Player 2 Won!";
            else
                label1.Text = "It's a Tie!";
            label2.Text = "" + score1;
            label3.Text = "" + score2;
            mSoundPlayer = new SoundPlayer(Properties.Resources.victory);
            mSoundPlayer.Play();
        }

        private void VictoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mSoundPlayer.Stop();
        }
    }
}
