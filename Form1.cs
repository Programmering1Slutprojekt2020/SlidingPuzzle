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
        int[,] koordinater = new int[4, 4];

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush grå = new SolidBrush(Color.LightGray);
            SolidBrush svart = new SolidBrush(Color.Black);
            Font font = new Font(FontFamily.GenericSansSerif,20,FontStyle.Regular);
            for(int x = 0; x < 4; x++)
            {
                for(int y = 0; y < 4; y++)
                {
                    if (koordinater[x, y] != 0)
                    {
                        g.FillRectangle(grå, x * 50, y * 50, 49, 49);
                        g.DrawString(koordinater[x, y].ToString(),font, svart, x * 50, y * 50);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            panel1.Invalidate();
        }
    }
}
