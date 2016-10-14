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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.newGameMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.optionsMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.exitMenuItem = new System.Windows.Forms.MenuItem();
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 456);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem newGameMenuItem;
        private System.Windows.Forms.MenuItem optionsMenuItem;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem exitMenuItem;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem3;
    }
}

