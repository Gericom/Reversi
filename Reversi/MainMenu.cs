using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reversi
{
	public class MainMenu : System.Windows.Forms.MainMenu {
        //This prevents a neasty visual studio designer bug that makes your form smaller everytime you view it,
        //when it has a main menu on it
		private System.ComponentModel.IContainer iContainer;
		public MainMenu(System.ComponentModel.IContainer iContainer)
		{
			this.iContainer = iContainer;
		}
	}
}
