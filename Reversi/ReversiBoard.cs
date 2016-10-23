using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Reversi
{
    public partial class ReversiBoard : UserControl
    {
        public enum Turn
        {
            Player1,
            Player2
        }

		public delegate void OnPlayerSwitchEventHandler(Turn newPlayer);
		public event OnPlayerSwitchEventHandler PlayerSwitch;

        public delegate void OnPassRequiredEventHandler();
        public event OnPassRequiredEventHandler PassRequired;

        public delegate void OnGameEndEventHandler();
        public event OnGameEndEventHandler GameEnd;

        public ReversiBoard()
        {
            InitializeComponent();
        }

        public Turn WhichPlayersTurn { get; set; } = Turn.Player1;

        public Color Player1Color { get; set; } = Color.Black;
        public Color Player2Color { get; set; } = Color.White;

        private ReversiGame mGame;

        [Browsable(false)]
        public ReversiGame Game
        {
            get
            {
                return mGame;
            }
            set
            {
                mGame = value;
                WhichPlayersTurn = Turn.Player1;
                if (mGame != null)
                    CalculateFieldEnclosures();
            }
        }

        private ReversiGame.ReversiField[,][] mEnclosuresForFields = null;

        private void CalculateFieldEnclosures()
        {
            mEnclosuresForFields = new ReversiGame.ReversiField[Game.BoardSize, Game.BoardSize][];
            bool shouldpass = true;
            for (int y = 0; y < Game.BoardSize; y++)
            {
                for (int x = 0; x < Game.BoardSize; x++)
                {
                    mEnclosuresForFields[x, y] =
                        Game.GetEnclosedFields(x, y, WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2);
                    if (mEnclosuresForFields[x, y].Length > 0)
                        shouldpass = false;
                }
            }
            if (shouldpass && PassRequired != null)
                PassRequired.Invoke();
        }

        private void GetBoardDimension(out int xOffset, out int yOffset, out int fieldSize, out int fontsize)
		{
			int size = (Width < Height ? Width : Height);
			int borderSize = size / 15;
			size -= borderSize * 2;
			fieldSize = size / Game.BoardSize;
			fontsize = fieldSize / 6;
			xOffset = (Width - fieldSize * Game.BoardSize) / 2;
			yOffset = (Height - fieldSize * Game.BoardSize) / 2;
		}

        private void ReversiBoard_Paint(object sender, PaintEventArgs e)
        {
            if (Game == null || mEnclosuresForFields == null)
                return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            int size = (Width < Height ? Width : Height);
            e.Graphics.FillRectangle(
                new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.FromArgb(201, 115, 64), Color.FromArgb(140, 73, 30)),
                (Width - size) / 2 + size / 30, (Height - size) / 2 + size / 30, size - size / 15, size - size / 15);
			int fieldSize, fontsize, xOffset, yOffset;
			GetBoardDimension(out xOffset, out yOffset, out fieldSize, out fontsize);
			e.Graphics.TranslateTransform(xOffset, yOffset);
            for (int y = 0; y < Game.BoardSize; y++)
            {
				var s = e.Graphics.Save();
				for (int x = 0; x < Game.BoardSize; x++)
                {
					
                    if (((x & 1) ^ (y & 1)) == 1)
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 143, 54)), 0, 0, fieldSize, fieldSize);
                    else
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 183, 54)),0, 0, fieldSize, fieldSize);
                    if (Game[x, y].Content != ReversiGame.ReversiField.FieldContent.Empty)
                    {
                        if (Game[x, y].Content == ReversiGame.ReversiField.FieldContent.Player1)
                            e.Graphics.FillEllipse(
                                new LinearGradientBrush(
                                    new Point( 0 - fieldSize / 2,    - fieldSize / 2),
                                    new Point( 0 + fieldSize,     fieldSize),
                                    Color.White,
                                    Color.FromArgb((int)(Player1Color.R * 0.75), (int)(Player1Color.G * 0.75), (int)(Player1Color.B * 0.75))),
                                0 + fieldSize / 16,    + fieldSize / 16, fieldSize - fieldSize / 8, fieldSize - fieldSize / 8);
                        else
                            e.Graphics.FillEllipse(
                                new LinearGradientBrush(
                                    new Point( 0 - fieldSize / 2,    - fieldSize / 2),
                                    new Point( 0 + fieldSize,    + fieldSize),
                                    Color.White,
                                    Color.FromArgb((int)(Player2Color.R * 0.75), (int)(Player2Color.G * 0.75), (int)(Player2Color.B * 0.75))),
                                 0 + fieldSize / 16,fieldSize / 16, fieldSize - fieldSize / 8, fieldSize - fieldSize / 8);
                    }
					if (mEnclosuresForFields[x, y].Length > 0)
                    {
                        e.Graphics.FillEllipse(Brushes.LightGreen,  0 + fieldSize / 4,    + fieldSize / 4, fieldSize - fieldSize / 2, fieldSize - fieldSize / 2);
                        e.Graphics.DrawString(mEnclosuresForFields[x, y].Length.ToString(), new Font("Segoe UI Semibold", fontsize), Brushes.Black,
                            new RectangleF( 0 + fieldSize / 4,    + fieldSize / 4, fieldSize - fieldSize / 2, fieldSize - fieldSize / 2),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
                    }
					e.Graphics.TranslateTransform(fieldSize, 0);
				}
				e.Graphics.Restore(s);
				e.Graphics.TranslateTransform(0, fieldSize);
			}
		}

        private void ReversiBoard_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ReversiBoard_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void ReversiBoard_MouseClick(object sender, MouseEventArgs e)
        {
			int fieldSize, fontsize, xOffset, yOffset;
			GetBoardDimension(out xOffset, out yOffset, out fieldSize, out fontsize);
			int realx = e.X - xOffset;
            int realy = e.Y - yOffset;
            int fieldx = realx / fieldSize;
            int fieldy = realy / fieldSize;
            if (fieldx < 0 || fieldy < 0 || fieldx >= Game.BoardSize || fieldy >= Game.BoardSize)
                return;
            ReversiGame.ReversiField[] fields = Game.GetEnclosedFields(fieldx, fieldy, WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2);
            if (fields.Length == 0)
                return;
            foreach (ReversiGame.ReversiField f in fields)
                f.Reverse();
            Game[fieldx, fieldy].Content = WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2;
            WhichPlayersTurn = WhichPlayersTurn == Turn.Player1 ? Turn.Player2 : Turn.Player1;
            CalculateFieldEnclosures();
            if ( PlayerSwitch != null )
				PlayerSwitch.Invoke(WhichPlayersTurn);
            Invalidate();
        }
    }
}

