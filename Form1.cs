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
        //int[,] test1 = new int[4, 4] { { 0, 4, 8, 12 }, { 1, 5, 9, 13 }, { 2, 6, 10, 14 }, { 3, 7, 11, 15 } };
        int[,] test2 = new int[4, 4] { { 1, 5, 9, 13 }, { 2, 6, 10, 14 }, { 3, 7, 11, 15 }, { 4, 8, 12, 0 } };

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush grå = new SolidBrush(Color.LightGray);
            SolidBrush svart = new SolidBrush(Color.Black);
            Font font = new Font(FontFamily.GenericSansSerif,25,FontStyle.Regular);
            if (vinst)
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
        private void test()
        {
            vinst = true;
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (koordinater[x, y] != test2[x, y])
                    {
                        vinst = false;
                        break;
                    }
                }
            }
            panel1.Invalidate();
        }

    }
}
