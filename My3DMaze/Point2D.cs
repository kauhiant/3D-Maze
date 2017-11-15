using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class Point2D
    {
        public int X;/*{ get; private set; }*/
        public int Y;/*{ get; private set; }*/

        public Point2D(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void set(int X , int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
