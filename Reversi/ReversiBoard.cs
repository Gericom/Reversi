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
using System.Threading;

namespace Reversi
{
    public partial class ReversiBoard : UserControl
    {
        public enum Turn
        {
            Player1,
            Player2
        }

        //events for a couple of game events
        public delegate void OnPlayerSwitchEventHandler(Turn newPlayer);
        public event OnPlayerSwitchEventHandler PlayerSwitch;

        public delegate void OnPassRequiredEventHandler();
        public event OnPassRequiredEventHandler PassRequired;

        public delegate void OnGameEndEventHandler();
        public event OnGameEndEventHandler GameEnd;

        public Turn WhichPlayersTurn { get; set; } = Turn.Player1;

        public Color Player1Color { get; set; } = Color.Black;
        public Color Player2Color { get; set; } = Color.White;

        private ReversiGame mGame;

        [Browsable(false)]
        public ReversiGame Game
        {
            get { return mGame; }
            set
            {
                mGame = value;
                //reset stuff when the game instance is changed
                WhichPlayersTurn = Turn.Player1;
                if (mGame != null)
                {
                    mFieldAnimators = new Animator<float>[mGame.BoardSize, mGame.BoardSize];
                    CalculateFieldEnclosures();
                }
            }
        }

        private ReversiGame.ReversiField[,][] mEnclosuresForFields = null;

        private bool mBusyAnimating = false;
        private Animator<float>[,] mFieldAnimators;

        private ReversiGame.ReversiField mMouseOverField;
        private bool mHasPassed;

        public ReversiBoard()
        {
            InitializeComponent();
        }

        private void CalculateFieldEnclosures()
        {
            //Build up the mEnclosuresForFields table with the fields that will be enclosed
            //if a stone was placed in that field
            mEnclosuresForFields = new ReversiGame.ReversiField[Game.BoardSize, Game.BoardSize][];
            bool shouldpass = true;
            int nremptyspaces = 0;
            int nrmycolor = 0;
            for (int y = 0; y < Game.BoardSize; y++)
            {
                for (int x = 0; x < Game.BoardSize; x++)
                {
                    if (Game[x, y].Content == ReversiGame.ReversiField.FieldContent.Empty)
                        nremptyspaces++;
                    else if (Game[x, y].Content == (WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2))
                        nrmycolor++;
                    mEnclosuresForFields[x, y] =
                        Game.GetEnclosedFields(x, y, WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2);
                    if (mEnclosuresForFields[x, y].Length > 0)
                        shouldpass = false;
                }
            }
            //Determine if the game has ended, or if just a pass is required
            if ((mHasPassed || nremptyspaces == 0 || nrmycolor == 0) && shouldpass && GameEnd != null)
                GameEnd.Invoke();
            else if (shouldpass && PassRequired != null)
                PassRequired.Invoke();
        }

        private void GetBoardDimensions(out int xOffset, out int yOffset, out int fieldSize, out int fontsize)
        {
            int size = (Width < Height ? Width : Height);
            int borderSize = size / 15;
            size -= borderSize * 2;
            fieldSize = size / Game.BoardSize;
            fontsize = fieldSize / 6;
            //font size can't be smaller than 1
            if (fontsize < 1)
                fontsize = 1;
            xOffset = (Width - fieldSize * Game.BoardSize) / 2;
            yOffset = (Height - fieldSize * Game.BoardSize) / 2;
        }

        private ReversiGame.ReversiField GetReversiFieldByMouseCoords(int x, int y)
        {
            int fieldSize, fontsize, xOffset, yOffset;
            GetBoardDimensions(out xOffset, out yOffset, out fieldSize, out fontsize);
            int realx = x - xOffset;
            int realy = y - yOffset;
            int fieldx = realx / fieldSize;
            int fieldy = realy / fieldSize;
            if (fieldx < 0 || fieldy < 0 || fieldx >= Game.BoardSize || fieldy >= Game.BoardSize)
                return null;
            return mGame[fieldx, fieldy];
        }

        private void DrawReversiStone(Graphics g, Turn player, int fieldSize)
        {
            if (player == Turn.Player1)
                g.FillEllipse(
                    new LinearGradientBrush(
                        new Point(-fieldSize / 2, -fieldSize / 2),
                        new Point(fieldSize, fieldSize),
                        Color.White,
                        Color.FromArgb((int)(Player1Color.R * 0.75), (int)(Player1Color.G * 0.75), (int)(Player1Color.B * 0.75))),
                    fieldSize / 16, fieldSize / 16, fieldSize - fieldSize / 8, fieldSize - fieldSize / 8);
            else
                g.FillEllipse(
                    new LinearGradientBrush(
                        new Point(-fieldSize / 2, -fieldSize / 2),
                        new Point(fieldSize, fieldSize),
                         Color.White,
                        Color.FromArgb((int)(Player2Color.R * 0.75), (int)(Player2Color.G * 0.75), (int)(Player2Color.B * 0.75))),
                    fieldSize / 16, fieldSize / 16, fieldSize - fieldSize / 8, fieldSize - fieldSize / 8);
        }

        private void ReversiBoard_Paint(object sender, PaintEventArgs e)
        {
            if (Game == null || mEnclosuresForFields == null)
                return;
            //make it look nice
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            int size = (Width < Height ? Width : Height);
            e.Graphics.FillRectangle(
                new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.FromArgb(201, 115, 64), Color.FromArgb(140, 73, 30)),
                (Width - size) / 2 + size / 30, (Height - size) / 2 + size / 30, size - size / 15, size - size / 15);
            int fieldSize, fontsize, xOffset, yOffset;
            GetBoardDimensions(out xOffset, out yOffset, out fieldSize, out fontsize);
            //translate to the top-left of the board
            e.Graphics.TranslateTransform(xOffset, yOffset);
            for (int y = 0; y < Game.BoardSize; y++)
            {
                //store the transformation as it is now
                var s = e.Graphics.Save();
                for (int x = 0; x < Game.BoardSize; x++)
                {
                    //alternating dark and light fields
                    if (((x & 1) ^ (y & 1)) == 1)
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 143, 54)), 0, 0, fieldSize, fieldSize);
                    else
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0, 183, 54)), 0, 0, fieldSize, fieldSize);
                    //Circle indicating legal fields and the number of stones you would enclose
                    if (!mBusyAnimating && mEnclosuresForFields[x, y].Length > 0)
                    {
                        e.Graphics.FillEllipse(Brushes.LightGreen, fieldSize / 4, fieldSize / 4, fieldSize - fieldSize / 2, fieldSize - fieldSize / 2);
                        e.Graphics.DrawString(mEnclosuresForFields[x, y].Length.ToString(), new Font("Segoe UI Semibold", fontsize), Brushes.Black,
                            new RectangleF(fieldSize / 4, fieldSize / 4, fieldSize - fieldSize / 2, fieldSize - fieldSize / 2),
                            new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });

                        //Legal field you're currently hovering
                        if (mGame[x, y] == mMouseOverField)
                            DrawReversiStone(e.Graphics, WhichPlayersTurn, fieldSize);
                    }
                    if (Game[x, y].Content != ReversiGame.ReversiField.FieldContent.Empty)
                    {
                        float xscale = 1;
                        //store the transformation as it is now
                        GraphicsState gs2 = e.Graphics.Save();
                        //The animator changes the x scale if not null
                        if (mFieldAnimators[x, y] != null)
                            xscale = mFieldAnimators[x, y].GetLatestValue();
                        //scale = 0 means it's not visible, preventing a crash
                        if (xscale != 0)
                        {
                            //scale from the middle of the field
                            e.Graphics.TranslateTransform(fieldSize / 2f, fieldSize / 2f);
                            e.Graphics.ScaleTransform(xscale, 1);
                            e.Graphics.TranslateTransform(-fieldSize / 2f, -fieldSize / 2f);
                            DrawReversiStone(e.Graphics, (Game[x, y].Content == ReversiGame.ReversiField.FieldContent.Player1 ? Turn.Player1 : Turn.Player2), fieldSize);
                        }
                        //restore the transformation
                        e.Graphics.Restore(gs2);
                    }
                    //indicate the stones that will flip when hovering over a legal field
                    if (!mBusyAnimating && mMouseOverField != null && mEnclosuresForFields[mMouseOverField.X, mMouseOverField.Y].Contains(Game[x, y]))
                    {
                        Color c = (WhichPlayersTurn == Turn.Player1 ? Color.FromArgb(192, 0, 0, 0) : Color.FromArgb(128, 255, 255, 255));
                        e.Graphics.FillEllipse(new SolidBrush(c), fieldSize / 4, fieldSize / 4, fieldSize - fieldSize / 2, fieldSize - fieldSize / 2);
                    }
                    e.Graphics.TranslateTransform(fieldSize, 0);
                }
                //restore the transformation
                e.Graphics.Restore(s);
                //move to the next row
                e.Graphics.TranslateTransform(0, fieldSize);
            }
        }

        private void ReversiBoard_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ReversiBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (mBusyAnimating)
                return;

            var field = GetReversiFieldByMouseCoords(e.X, e.Y);
            ReversiGame.ReversiField[] fields = Game.GetEnclosedFields(field.X, field.Y, WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2);
            if (fields.Length == 0)
                return;

            field.Content = WhichPlayersTurn == Turn.Player1 ? ReversiGame.ReversiField.FieldContent.Player1 : ReversiGame.ReversiField.FieldContent.Player2;
            Invalidate();

            //start the flipping animation
            mBusyAnimating = true;
            new Thread(FieldAnimatorThread).Start(fields);
        }

        private void FieldAnimatorThread(object arg)
        {
            ReversiGame.ReversiField[] fields = (ReversiGame.ReversiField[])arg;
            bool[] isreversed = new bool[fields.Length];
            //setup the initial animation 1.0 -> 0.0 x scale
            foreach (ReversiGame.ReversiField f in fields)
            {
                mFieldAnimators[f.X, f.Y] = new Animator<float>(1f, 0f, 5);
            }
            while (true)
            {
                bool isfullyfinished = true;
                for (int i = 0; i < fields.Length; i++)
                {
                    if (!(isreversed[i] && mFieldAnimators[fields[i].X, fields[i].Y].IsFinished))
                        isfullyfinished = false;
                }
                if (isfullyfinished)
                    break;
                for (int i = 0; i < fields.Length; i++)
                {
                    float scale = mFieldAnimators[fields[i].X, fields[i].Y].AdvanceFrame();
                    if (scale == 0)
                    {
                        //reverse the stone and setup the second animation 0.0 -> 1.0 x scale
                        fields[i].Reverse();
                        mFieldAnimators[fields[i].X, fields[i].Y] = new Animator<float>(0f, 1f, 5);
                        mFieldAnimators[fields[i].X, fields[i].Y].AdvanceFrame();
                        isreversed[i] = true;
                    }
                }

                Invoke((Action)delegate
                {
                    Invalidate();
                });
                Thread.Sleep(50);
            }
            //reset the field animators to null
            foreach (ReversiGame.ReversiField f in fields)
            {
                mFieldAnimators[f.X, f.Y] = null;
            }
            //switch players
            mHasPassed = false;
            SwitchPlayer();
            Invoke((Action)delegate
            {
                Invalidate();
            });
            mBusyAnimating = false;
        }

        private void SwitchPlayer()
        {
            WhichPlayersTurn = WhichPlayersTurn == Turn.Player1 ? Turn.Player2 : Turn.Player1;
            CalculateFieldEnclosures();
            if (PlayerSwitch != null)
                PlayerSwitch.Invoke(WhichPlayersTurn);
        }

        public void PassTurn()
        {
            mHasPassed = true;
            SwitchPlayer();
            Invalidate();
        }

        private void ReversiBoard_MouseMove(object sender, MouseEventArgs e)
        {
            mMouseOverField = GetReversiFieldByMouseCoords(e.X, e.Y);
            Invalidate();
        }
    }
}

