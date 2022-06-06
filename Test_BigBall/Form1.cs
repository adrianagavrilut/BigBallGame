using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_BigBall;

namespace BigBallGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics g;
        List<Ball> balls;
        Random rnd;
        Timer myTimer;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rnd = new Random();
            balls = new List<Ball>();
        }

        private void buttonDrawCircle_Click(object sender, EventArgs e)
        {
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                balls.Add(new Ball(rnd.Next(25, panel1.Width - 50), rnd.Next(25, panel1.Height - 50), rnd.Next(1,4), rnd.Next(-2, 3), rnd.Next(-2, 3), rnd.Next(5, 40), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
            }
            Graphics g = panel1.CreateGraphics();
            panel1.Refresh();
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Draw(g);
                //verifici coliziunea
                balls[i].Move();
            }
        }

        private void Draw(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            panel1.Refresh();
            int[] shouldDestroy = new int[balls.Count];
            for(int i = 0; i < balls.Count; i++)
            {
                //verifica coliziunea intre bile
                for (int j = i + 1; j < balls.Count; j++)
                {
                    if (IsCollision(balls[i], balls[j]))
                    {
                        shouldDestroy[i] += balls[i].Colide(balls[j]);
                        shouldDestroy[j] += balls[j].Colide(balls[i]);
                    }
                }
                //verifica coliziunea cu peretele
                balls[i].WallColision(panel1.Width - 10, panel1.Height - 10);

                balls[i].Move();
                balls[i].Draw(g);
            }
            for(int i = 0; i< shouldDestroy.Length; i++)
            {
                if(shouldDestroy[i] > 0)
                {
                    balls.RemoveAt(i);
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            myTimer.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            myTimer = new Timer();
            myTimer.Interval =  2;
            myTimer.Tick += new System.EventHandler(Draw);
            myTimer.Enabled = true;
        }

        private bool IsCollision(Ball a, Ball b)
        {
            if (GetDistance(a.Centre, b.Centre) <= (a.Radius + b.Radius))
            {
                return true;
            }
            return false;
        }

        private double GetDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow((a.X - b.X), 2) + Math.Pow((a.Y - b.Y), 2));
        }
    }
}
