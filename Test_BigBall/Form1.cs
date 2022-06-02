using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigBallGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g;
        List<Tuple<int, int, int, int, int>> bile;
        //Tuple<int, int, int, int, int> bila;
        Random rndX;
        Random rndBila;
        Random rndColor;
        Pen pen;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rndX = new Random();
            rndBila = new Random();
            rndColor = new Random();
            bile = new List<Tuple<int, int, int, int, int>>();
        }

        private void buttonDrawCircle_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            int n = 25;
            for (int i = 0; i < n; i++)
            {
                Tuple<int, int, int, int, int>  bila = new Tuple<int, int, int, int, int>(rndBila.Next(50, panel1.Width - 50), rndBila.Next(50, panel1.Height - 50), 20, 20, rndColor.Next(0, 3));
                bile.Add(bila);
            }
            Timer myTimer = new Timer();
            myTimer.Interval = 20;
            myTimer.Tick += new System.EventHandler(Draw);
            //myTimer.Elapsed += OnTimedEvent;
            //myTimer.AutoReset = true;
            myTimer.Enabled = true;

        }

        private void Draw(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            panel1.Refresh();
            // = new Pen(Color.Black, 2);
            for(int i = 0; i < bile.Count; i++)
            {
                switch (bile[i].Item5)
                {
                    case 0:
                        pen = new Pen(Color.Black, 2);
                        break;
                    case 1:
                        pen = new Pen(Color.Red, 2);
                        break;
                    case 2:
                        pen = new Pen(Color.BlueViolet, 2);
                        break;
                    case 3:
                        pen = new Pen(Color.OrangeRed, 2);
                        break;
                    default:
                        break;
                }
                g.DrawEllipse(pen, bile[i].Item1, bile[i].Item2, bile[i].Item3, bile[i].Item4);
                bile[i] = new Tuple<int, int, int, int, int>(rndX.Next(bile[i].Item1 - 2, bile[i].Item1 + 2), rndX.Next(bile[i].Item2 - 2, bile[i].Item2 + 2), 15, 15, bile[i].Item5);
            }
            

        }

       
    }
}
