﻿using System;
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
    public partial class OptionsForm : Form
    {
        public int BoardSize { get; private set; }

        public OptionsForm(int boardSize)
        {
            BoardSize = boardSize;
            InitializeComponent();
            numericUpDown1.Value = BoardSize;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //multiple of 2
            numericUpDown1.Value = ((int)numericUpDown1.Value + 1) & ~1;
            BoardSize = (int)numericUpDown1.Value;
        }
    }
}
