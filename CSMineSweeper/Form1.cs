using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMineSweeper
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			foreach (Button c in Game.btns)
			{
				this.Controls.Add(c);
			}
		}


		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			Program.StF.Show();
		}
	}
}
