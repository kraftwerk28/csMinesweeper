using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMineSweeper
{
	public class Cell
	{
		public int Condition;
		public int Posx;
		public int Posy;
		public int Size;
		public Button b;
		public Cell(int size, int condition, int posx, int posy, string Name)
		{
			this.Size = size;
			this.Condition = condition;
			this.Posx = posx;
			this.Posy = posy;
			this.b = new Button();
			b.Size = new System.Drawing.Size(Size, Size);
			b.Location = new System.Drawing.Point(Posx, Posy);
			b.Name = Name;
		}
	}
}
