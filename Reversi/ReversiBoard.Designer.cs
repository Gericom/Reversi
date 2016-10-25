namespace Reversi
{
    partial class ReversiBoard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ReversiBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(91)))), ((int)(((byte)(36)))));
            this.DoubleBuffered = true;
            this.Name = "ReversiBoard";
            this.Size = new System.Drawing.Size(608, 366);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ReversiBoard_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ReversiBoard_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReversiBoard_MouseMove);
            this.Resize += new System.EventHandler(this.ReversiBoard_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
