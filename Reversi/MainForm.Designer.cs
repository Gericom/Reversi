namespace Reversi
{
    partial class MainForm
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.mainMenu1 = new Reversi.MainMenu(this.components);
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.newGameMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.optionsMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.reversiBoard2 = new Reversi.ReversiBoard();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.newGameMenuItem,
            this.menuItem6,
            this.menuItem3,
            this.optionsMenuItem,
            this.menuItem2,
            this.exitMenuItem});
			this.menuItem1.Text = "Game";
			// 
			// newGameMenuItem
			// 
			this.newGameMenuItem.Index = 0;
			this.newGameMenuItem.Shortcut = System.Windows.Forms.Shortcut.F2;
			this.newGameMenuItem.Text = "New Game";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.menuItem3.Text = "Statistics";
			// 
			// optionsMenuItem
			// 
			this.optionsMenuItem.Index = 3;
			this.optionsMenuItem.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.optionsMenuItem.Text = "Options";
			this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// exitMenuItem
			// 
			this.exitMenuItem.Index = 5;
			this.exitMenuItem.Text = "Exit";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
			// 
			// reversiBoard2
			// 
			this.reversiBoard2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(91)))), ((int)(((byte)(36)))));
			this.reversiBoard2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.reversiBoard2.Game = null;
			this.reversiBoard2.Location = new System.Drawing.Point(0, 0);
			this.reversiBoard2.Name = "reversiBoard2";
			this.reversiBoard2.Padding = new System.Windows.Forms.Padding(10);
			this.reversiBoard2.Player1Color = System.Drawing.Color.Black;
			this.reversiBoard2.Player2Color = System.Drawing.Color.White;
			this.reversiBoard2.Size = new System.Drawing.Size(617, 442);
			this.reversiBoard2.TabIndex = 1;
			this.reversiBoard2.WhichPlayersTurn = Reversi.ReversiBoard.Turn.Player1;
			this.reversiBoard2.PlayerSwitch += new Reversi.ReversiBoard.OnPlayerSwitchEventHandler(this.reversiBoard2_PlayerSwitch);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
			this.statusStrip1.Location = new System.Drawing.Point(0, 442);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(617, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.AutoSize = false;
			this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 17);
			this.toolStripStatusLabel1.Text = "Player 1: 2";
			this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.AutoSize = false;
			this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(100, 17);
			this.toolStripStatusLabel2.Text = "Player 2: 2";
			this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(371, 17);
			this.toolStripStatusLabel3.Spring = true;
			this.toolStripStatusLabel3.Text = "Player 1\'s Turn";
			this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolStripStatusLabel3.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(617, 464);
			this.Controls.Add(this.reversiBoard2);
			this.Controls.Add(this.statusStrip1);
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "Form1";
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem newGameMenuItem;
        private System.Windows.Forms.MenuItem optionsMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem3;

        private ReversiBoard reversiBoard2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
	}
}

