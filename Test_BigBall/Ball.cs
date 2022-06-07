using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Test_BigBall
{
    class Ball
    {
        #region Data
        private int X;
        private int Y;
        private int radius;
        private Color color;
        private int type;
        private int dx;
        private int dy;
        private int r;
        private int g;
        private int b;
        #endregion

        #region Properties
        /*public int Radius
        {
            get 
            {
                return radius;
            }
        } */
        public int Radius => radius;
        /*public Point Centre
        {
            get
            {
                return new Point(this.X + radius, this.Y + radius);
            }
        }
        */
        public Point Centre => new Point(this.X + radius, this.Y + radius);

        public bool IsMonster => this.type == 2;

        public bool IsRegular => this.type == 1;

        public bool IsRepellent => this.type == 3;
        #endregion

        #region Constructor
        public Ball(int x, int y, int type, int dx, int dy, int radius, int r, int g, int b)
        {
            this.X = x;
            this.Y = y;
            this.radius = radius;
            this.dx = dx;
            this.dy = dy;
            this.type = type;

            if (IsMonster)
            {
                this.color = Color.FromArgb(r, g, b);
                this.dx = 0;
                this.dy = 0;
            }
            if (IsRepellent)
            {
                this.color = Color.FromArgb(r, g, b);
            }
            if (IsRegular)
            {
                this.color = Color.FromArgb(r, g, b);
            }
        }
        #endregion

        #region Methods
        public void Draw(Graphics g)
        {
            SolidBrush solidBrush = new SolidBrush(this.color);
            g.FillEllipse(solidBrush, this.X, this.Y, this.radius * 2, this.radius * 2);
            string[] drawString = { "Reg", "Rep", "Mon" };
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            if (IsMonster)
                g.DrawString(drawString[2], drawFont, drawBrush, this.X, this.Y);
            if (IsRepellent)
                g.DrawString(drawString[1], drawFont, drawBrush, this.X, this.Y);
            if (IsRegular)
                g.DrawString(drawString[0], drawFont, drawBrush, this.X, this.Y);
        }

        public void Move()
        {
            this.X += this.dx;
            this.Y += this.dy;
        }

        public void WallCollision(int w, int h)
        {
            if (this.X + 2 * this.radius >= w )//perete dreapta
            {
                this.dx = - this.dx;
            }
            if (this.Y + 2 * this.radius >= h )//perete jos
            {
                this.dy = -this.dy;
            }
            if (this.X <= 0)//perete stanga
            {
                this.dx = -this.dx;
            }
            if (this.Y <= 0)//perete sus
            {
                this.dy = -this.dy;
            }
        }

        public int Colide(Ball target)
        {
            if(this.IsRegular && target.IsMonster)
            {
                //regular ball va dispărea din sistem
                return 1;
            }
            if (this.IsMonster && target.IsRegular)
            {
                //raza lui Monster ball crește cu raza bilei înghițite
                this.radius += target.Radius;
            }
            if (this.IsRegular && target.IsRegular)
            {
                if (this.radius > target.radius)
                {
                    //raza bilei mari crește cu raza bilei mici si
                    //culoarea bilei mari se schimbă prin combinarea culorilor celor două bile proporțional cu dimensiunea lor
                    this.radius += target.radius;
                    int proportion = this.radius / target.radius;
                    this.r = proportion * this.r + target.r;
                    this.g = proportion * this.g + target.g;
                    this.b = proportion * this.b + target.b;
                }
                else if(this.radius < target.radius)
                {
                    // bila mică dispare din sistem
                    return 1;
                }
            }
            if (this.IsRegular && target.IsRepellent)
            {
                //regular ball își schimbă sensul de deplasare
                this.dx = -this.dx;
                this.dy = -this.dy;
            }
            if (this.IsRepellent && target.IsRegular)
            {
                //repelent ball preia culoarea lui regular ball
                this.r += target.r;
                if (this.r > 255)
                    this.r %= 255;
                this.g += target.g;
                if (this.g > 255)
                    this.g %= 255;
                this.b += target.b;
                if (this.b > 255)
                    this.b %= 255;
            }
            if (this.IsRepellent && target.IsMonster)
            {
                //raza lui repelent ball se înjumătățește
                this.radius /= 2;
                if(this.radius < 2)
                {
                    this.radius = 2;
                }
            }
            if (this.IsRepellent && target.IsRepellent)
            {
                //își vor interschimba culorile
                int auxR = target.r;
                target.r = this.r;
                this.r = auxR;
                int auxG = target.g;
                target.g = this.g;
                this.g = auxG;
                int auxB = target.b;
                target.b = this.b;
                this.b = auxB;
            }
            return 0;
        }
        #endregion
    }
}
