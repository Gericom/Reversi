using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class ReversiBoard : UserControl
    {
        public ReversiBoard()
        {

            InitializeComponent();
        }

        public Color Player1Color { get; set; } = Color.Black;
        public Color Player2Color { get; set; } = Color.White;
        [Browsable(false)]
        public ReversiGame Game { get; set; }

        private void ReversiBoard_Paint(object sender, PaintEventArgs e)
        {
            if ( Game == null )
                return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            for ( int y = 0; y < Game.BoardSize; y++ )
            {
                for ( int x = 0; x < Game.BoardSize; x++ )
                {
                    if ( ( ( x & 1 ) ^ ( y & 1 ) ) == 1 )
                        e.Graphics.FillRectangle(Brushes.ForestGreen, x * 64, y * 64, 64, 64);
                    else
                        e.Graphics.FillRectangle(Brushes.DarkSeaGreen, x * 64, y * 64, 64, 64);
                }
            
            }
        }
    }
}
