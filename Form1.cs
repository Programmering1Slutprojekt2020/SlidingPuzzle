using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlidingPuzzle
{
    public partial class Form1 : Form
    {
        int förflyttningar = 0;
        int[,] koordinater = new int[4, 4];
        bool vinst = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush grå = new SolidBrush(Color.LightGray);
            SolidBrush svart = new SolidBrush(Color.Black);
            Font font = new Font(FontFamily.GenericSansSerif,24,FontStyle.Regular);
            if (vinst==true)
            {
                g.DrawString("Vinst", font, svart, 50, 50);
            }
            else
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        if (koordinater[x, y] != 0)
                        {
                            g.FillRectangle(grå, x * 50, y * 50, 49, 49);
                            g.DrawString(koordinater[x, y].ToString(), font, svart, x * 50, y * 50);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            förflyttningar = 0;
            label1.Text = "Antal förflyttningar: " + förflyttningar;
            vinst = false;
            Random r = new Random();
            List<int> tal = new List<int>();
            int index;
            for(int i = 0; i < 16; i++)
            {
                tal.Add(i);
            }
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    index = r.Next(0, tal.Count);
                    koordinater[x, y] = tal.ElementAt(index);
                    tal.RemoveAt(index);
                }
            }
            test();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 50;
            int y = e.Y / 50;
            if (x < 3) {
                if (koordinater[x + 1, y] == 0)
                {
                    koordinater[x + 1, y] = koordinater[x, y];
                    koordinater[x, y] = 0;
                    förflyttningar++;
                }
            }
            if (x > 0)
            {
                if (koordinater[x - 1, y] == 0)
                {
                    koordinater[x - 1, y] = koordinater[x, y];
                    koordinater[x, y] = 0;
                    förflyttningar++;
                }
            }
            if (y < 3)
            {
                if (koordinater[x, y + 1] == 0)
                {
                    koordinater[x, y + 1] = koordinater[x, y];
                    koordinater[x, y] = 0;
                    förflyttningar++;
                }
            }
            if (y > 0)
            {
                if (koordinater[x, y - 1] == 0)
                {
                    koordinater[x, y - 1] = koordinater[x, y];
                    koordinater[x, y] = 0;
                    förflyttningar++;
                }
            }
            label1.Text = "Antal förflyttningar: " + förflyttningar;
            test();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    koordinater[x, y] = (y * 4 + x);
                }
            }
            test();
        }

        private void test()
        {
            int temp = 0;
            int rätt = 0;
            for (int b = 0; b < 4; b++)
            {
                for (int a = 0; a < 4; a++)
                {
                    if (koordinater[a, b] >= temp) rätt++;
                    if (temp == 15 && koordinater[a, b] == 0) rätt++;
                    temp = koordinater[a, b];
                }
                if (rätt >= 16) vinst = true;
            }
            panel1.Invalidate();
        }
    }
}
