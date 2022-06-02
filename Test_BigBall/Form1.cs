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

        Tuple<int, int, int, int> bila;
        Random rndX;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            rndX = new Random();
        }

        private void buttonDrawCircle_Click(object sender, EventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            bila = new Tuple<int, int, int, int>(250, 300, 15, 15);
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
            Pen pen = new Pen(Color.Black, 2);
            g.DrawEllipse(pen, bila.Item1, bila.Item2, bila.Item3, bila.Item4);

            
            bila = new Tuple<int, int, int, int>(rndX.Next(bila.Item1 - 2, bila.Item1 + 2), rndX.Next(bila.Item2 - 2, bila.Item2 + 2), 15, 15);

        }
    }
}
