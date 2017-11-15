using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class Point3D
    {
        //軸
        public int x { get; private set; }
        public int y { get; private set; }
        public int z { get; private set; }
        //平面
        public Point2D plane;

        public Point3D(int x,int y,int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void setPoint(int x=0,int y=0,int z = 0)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int distanceTo(Point3D target)
        {
            int tmp = 0;
            int min = int.MaxValue;
            tmp = Math.Abs(this.x - target.x) ;
            min = min < tmp ? min : tmp;
            tmp = Math.Abs(this.y - target.y);
            min = min < tmp ? min : tmp;
            tmp = Math.Abs(this.z - target.z);
            min = min < tmp ? min : tmp;

            return min;
        }

        public override string ToString()
        {
            return "X=" + x + " ,Y=" + y + " ,Z=" + z;
        }

        public bool inRangeX(int a,int b)
        {
            if(a == b)
            {
                if (x == a) return true;
                else return false;
            }
            else if (a < b)
            {
                int tmp = a;
                a = b;
                b = tmp;
            }
            return (x >= a && x <= b);
        }
    }
}
