using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace CSMineSweeper
{
	public static class Game // 0-8: count of mines nearby; 9 - mine inside;
	{
		public static int[,] Grid;
		public static List<int[]> mines = new List<int[]>();
		public static List<Button> btns = new List<Button>();
		private static Random rand = new Random();


		public static void Generate(int width, int height, int ButtonSize, FlatStyle button_style, int MineCount)
		{
			Grid = new int[width, height];
			btns.Clear();
			mines.Clear();
			GenerateMines(MineCount);
			CheckNearby();
			GenerateButtons(ButtonSize, button_style);
		}

		private static void GenerateButtons(int button_size, FlatStyle B_style)
		{
			for (int x = 0; x < Grid.GetLength(0); x++)
			{
				for (int y = 0; y < Grid.GetLength(1); y++)
				{
					Button b = new Button();
					b.Size = new Size(button_size, button_size);
					b.Location = new Point(x * button_size, y * button_size);
					b.Name = x.ToString() + " " + y.ToString();
					b.MouseDown += B_MouseDown;
					b.Click += B_Click;
					b.BackColor = Color.LightSkyBlue;
					b.FlatStyle = B_style;
					btns.Add(b);
				}
			}

		}

		private static void GenerateMines(int count)
		{
			int cnt = count;
			if (count < 1)
				cnt = 1;
			if (count > Grid.Length)
				cnt = Grid.Length - 1;
			int x, y;
			for (int i = 0; i < cnt; i++)
			{
				do
				{
					x = rand.Next(0, Grid.GetLength(0));
					y = rand.Next(0, Grid.GetLength(1));
				}
				while (Mines_Contains(new int[] { x, y }));

				mines.Add(new int[] { x, y });
				Grid[mines[mines.Count - 1][0], mines[mines.Count - 1][1]] = 9;
			}
		}


		static bool Mines_Contains(int[] m)
		{
			bool c = false;
			foreach (int[] i in mines)
			{
				if (i[0] == m[0] && i[1] == m[1])
				{
					c = true;
					break;
				}

			}
			return c;
		}

		static void CheckNearby()
		{
			int w = Grid.GetLength(0) - 1;
			int h = Grid.GetLength(1) - 1;
			foreach (int[] m in mines)
			{
				if (m[0] > 0)
					Grid[m[0] - 1, m[1]]++;
				if (m[0] < w)
					Grid[m[0] + 1, m[1]]++;
				if (m[1] > 0)
					Grid[m[0], m[1] - 1]++;
				if (m[1] < h)
					Grid[m[0], m[1] + 1]++;

				if (m[0] > 0 && m[1] > 0)
					Grid[m[0] - 1, m[1] - 1]++;
				if (m[0] > 0 && m[1] < h)
					Grid[m[0] - 1, m[1] + 1]++;
				if (m[0] < w && m[1] > 0)
					Grid[m[0] + 1, m[1] - 1]++;
				if (m[0] < w && m[1] < h)
					Grid[m[0] + 1, m[1] + 1]++;

			}
		}

		static void CheckWin()
		{
			int cnt = 0;
			foreach (Button b in btns)
			{
				if (b.BackColor == Color.Lime)
					cnt++;
			}
			if (Grid.Length - cnt == mines.Count)
			{
				MessageBox.Show("You Won, nigga!!!");
				Program.StF.Show();
				Program.F1.Hide();
				Program.F1 = null;
			}
		}

		private static void ClickNearby(Button b)
		{
			List<Button> near_button = new List<Button>(8);

			int w = Grid.GetLength(0) - 1;
			int h = Grid.GetLength(1) - 1;

			int[] m = ButtonToCoord(b);
			if (m[0] > 0)
				near_button.Add(B(m[0] - 1, m[1]));
			if (m[0] < w)
				near_button.Add(B(m[0] + 1, m[1]));
			if (m[1] > 0)
				near_button.Add(B(m[0], m[1] - 1));
			if (m[1] < h)
				near_button.Add(B(m[0], m[1] + 1));

			if (m[0] > 0 && m[1] > 0)
				near_button.Add(B(m[0] - 1, m[1] - 1));
			if (m[0] > 0 && m[1] < h)
				near_button.Add(B(m[0] - 1, m[1] + 1));
			if (m[0] < w && m[1] > 0)
				near_button.Add(B(m[0] + 1, m[1] - 1));
			if (m[0] < w && m[1] < h)
				near_button.Add(B(m[0] + 1, m[1] + 1));

			int cnt = 0;
			foreach (Button _b in near_button)
			{
				if (_b != null)
					if (_b.BackColor == Color.Violet)
						cnt++;
			}
			if (cnt == Cell(b))
				foreach (Button _b in near_button)
				{
					if (_b != null)
						_b.PerformClick();
				}
			//foreach (Button _b in near_button)
			//{
			//	if (_b != null)
			//		_b.PerformClick();
			//}
		}

		private static void Clck(int x, int y)
		{
			if (btns[Grid.GetLength(1) * x + y].BackColor != Color.Lime)
				btns[Grid.GetLength(1) * x + y].PerformClick();
		}

		private static Button B(int x, int y)
		{
			if (btns[Grid.GetLength(1) * x + y].BackColor != Color.Lime)
				return btns[Grid.GetLength(1) * x + y];
			else
				return null;
		}


		private static void B_Click(object sender, EventArgs e)
		{

			Button b = (Button)sender;

			if (b.BackColor != Color.Violet)
			{
				if (Cell(b) > 8)
				{
					b.BackColor = Color.Red;
					MessageBox.Show("You're Loserrr!");
					Application.Restart();
				}
				else if (Cell(b) == 0)
				{
					b.BackColor = Color.Lime;
					ClickNearby(b);
				}
				else
				{
					b.Text = Cell(b).ToString();
					b.BackColor = Color.Lime;
				}
			}
			CheckWin();
		}

		private static void B_MouseDown(object sender, MouseEventArgs e)// LightSkyBlue Red Lime Violet
		{
			Button b = (Button)sender;
			if (e.Button == MouseButtons.Left)
			{
				if (b.BackColor == Color.LightSkyBlue)
				{
					if (Cell(b) > 8)
					{
						b.BackColor = Color.Red;
						MessageBox.Show("You're Loserrr!");
						Program.StF.Show();
						Program.F1.Hide();
					}
					else if (Cell(b) == 0)
					{
						b.BackColor = Color.Lime;
						ClickNearby(b);
					}
					else
						b.Text = Cell(b).ToString();
				}
				else if (b.BackColor == Color.Lime)
					ClickNearby(b);


				CheckWin();
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (b.BackColor == Color.LightSkyBlue)
					b.BackColor = Color.Violet;
				else if (b.BackColor == Color.Violet)
					b.BackColor = Color.LightSkyBlue;

				CheckWin();
			}
		}


		private static int[] ButtonToCoord(Button b)
		{
			return new int[] { Int32.Parse(b.Name.Split(new char[] { ' ' }, 2)[0]), Int32.Parse(b.Name.Split(new char[] { ' ' }, 2)[1]) };
		}

		private static int Cell(Button _button)
		{
			return Grid[Int32.Parse(_button.Name.Split(new char[] { ' ' }, 2)[0]), Int32.Parse(_button.Name.Split(new char[] { ' ' }, 2)[1])];
		}
	}
}
