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
        SoundPlayer mSoundPlayer;

        public VictoryForm()
        {
            InitializeComponent();
            mSoundPlayer = new SoundPlayer(Properties.Resources.victory);
            mSoundPlayer.Play();
        }
    }
}
