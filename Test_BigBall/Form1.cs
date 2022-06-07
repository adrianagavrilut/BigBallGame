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
        Bitmap bmp;
        List<Ball> balls;
        Random rnd;
        Timer myTimer;

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            rnd = new Random();
            balls = new List<Ball>();
        }

        private void buttonDrawCircle_Click(object sender, EventArgs e)
        {
            int n = 15;
            for (int i = 0; i < n; i++)
            {
                bool ok = true;
                do
                {
                    Ball myBall = new Ball(rnd.Next(25, pictureBox1.Width - 50), rnd.Next(25, pictureBox1.Height - 50), rnd.Next(1, 4), rnd.Next(-2, 3), rnd.Next(-2, 3), rnd.Next(5, 40), rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
                    if (myBall.IsMonster)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (balls[j].IsMonster && IsCollision(myBall, balls[j])) //verifica coliziunea initiala a bilelor monster
                            {
                                ok = false;
                            }
                        }
                    }
                    if (ok)
                    {
                        balls.Add(myBall);
                    }
                } while (!ok);
            }
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].Draw(g);
                balls[i].Move();
            }
            pictureBox1.Image = bmp;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            myTimer = new Timer();
            myTimer.Interval = 3;
            myTimer.Tick += new System.EventHandler(Draw);
            myTimer.Enabled = true;
        }

        private void Draw(object sender, EventArgs e)
        {
            ClearImage();
            int[] shouldDestroy = new int[balls.Count]; //vector marcaj
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
                balls[i].WallCollision(pictureBox1.Width - 10, pictureBox1.Height - 10);

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
            for (int i = 0; i < balls.Count; i++)
            {
                if (NoMoreRegulars(balls))
                {
                    myTimer.Enabled = false;
                }
            }
            pictureBox1.Image = bmp;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            myTimer.Enabled = false;
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

        private bool NoMoreRegulars(List<Ball> balls)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i].IsRegular)
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearImage()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(BackColor);
        }
    }
}
