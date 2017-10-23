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
	public partial class StartForm : Form
	{
		delegate void StartGen(int _width, int _height, int _buttonsize, FlatStyle _buttonstyle, int _minecount);
		StartGen g = new StartGen(Game.Generate);
		public IAsyncResult Asyncer { get; set; }

		public StartForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			StartAsync();
			while (!Asyncer.IsCompleted) ;
			Program.F1 = null;
			Program.F1 = new Form1();
			Program.StF.Hide();
			Program.F1.Show();
		}

		private void numericUpDown3_ValueChanged(object sender, EventArgs e)
		{
			if (numericUpDown3.Value > numericUpDown1.Value * numericUpDown2.Value - 1)
				numericUpDown3.Value = numericUpDown1.Value * numericUpDown2.Value - 1;
		}

		private void numericUpDown4_ValueChanged(object sender, EventArgs e)
		{
			button2.Size = new Size((int)numericUpDown4.Value, (int)numericUpDown4.Value);
		}
		
		private FlatStyle Fs()
		{
			FlatStyle S = FlatStyle.Standard;
			if (radioButton1.Checked)
				S = FlatStyle.Flat;
			else if (radioButton2.Checked)
				S = FlatStyle.Popup;
			else if (radioButton3.Checked)
				S = FlatStyle.Standard;
			return S;
		}

		private void StartAsync()
		{
			Asyncer = g.BeginInvoke((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown4.Value, Fs(), (int)numericUpDown3.Value, null, null);
		}

		private void StartForm_Load(object sender, EventArgs e)
		{

		}
	}
}
