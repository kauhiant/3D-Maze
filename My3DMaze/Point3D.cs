using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    public enum Vector3D { Xplus, Xsub, Yplus, Ysub, Zplus, Zsub, Null }

    class Range3D
    {
        public Range1D xRange { get; private set; }
        public Range1D yRange { get; private set; }
        public Range1D zRange { get; private set; }
        public Range3D(Range1D xRange, Range1D yRange, Range1D zRange)
        {
            this.xRange = xRange;
            this.yRange = yRange;
            this.zRange = zRange;
        }
        public void setRange3D(Range1D xRange, Range1D yRange, Range1D zRange)
        {
            this.xRange = xRange;
            this.yRange = yRange;
            this.zRange = zRange;
        }
    }

    class Point3D
    {
        //軸
        public Point1D x { get; private set; }
        public Point1D y { get; private set; }
        public Point1D z { get; private set; }

        public int X { get { return x.value; } }
        public int Y { get { return y.value; } }
        public int Z { get { return z.value; } }
        //平面
        public Point2D plane;

        public Point3D(int x,int y,int z)
        {
            this.x = new Point1D(x);
            this.y = new Point1D(y);
            this.z = new Point1D(z);
        }

        public int distanceTo(Point3D target)
        {
            int tmp = 0;
            int min = int.MaxValue;
            tmp = this.x.distanceTo(target.x) ;
            min = min < tmp ? min : tmp;
            tmp = this.y.distanceTo(target.y);
            min = min < tmp ? min : tmp;
            tmp = this.z.distanceTo(target.z);
            min = min < tmp ? min : tmp;

            return min;
        }

        public override string ToString()
        {
            return "X=" + x + " ,Y=" + y + " ,Z=" + z;
        }
        //不包刮邊緣
        public bool inRange(Range3D range)
        {
            return (this.x.inRange(range.xRange) &&
                this.y.inRange(range.yRange) &&
                this.z.inRange(range.zRange));
        }

        public bool onEdge(Range3D range)
        {
            return (this.x.onEdge(range.xRange) ||
                this.y.onEdge(range.yRange) ||
                this.z.onEdge(range.zRange));
        }

        public void moveForward(Vector3D vector ,int distance)
        {
            switch (vector)
            {
                case Vector3D.Xplus:
                    x.add(distance);
                    break;

                case Vector3D.Xsub:
                    x.add(-distance);
                    break;

                case Vector3D.Yplus:
                    y.add(distance);
                    break;

                case Vector3D.Ysub:
                    y.add(-distance);
                    break;

                case Vector3D.Zplus:
                    z.add(distance);
                    break;

                case Vector3D.Zsub:
                    z.add(-distance);
                    break;
            }
        }

        public void moveBack(Vector3D vector, int distance)
        {
            switch (vector)
            {
                case Vector3D.Xplus:
                    x.add(-distance);
                    break;

                case Vector3D.Xsub:
                    x.add(distance);
                    break;

                case Vector3D.Yplus:
                    y.add(-distance);
                    break;

                case Vector3D.Ysub:
                    y.add(distance);
                    break;

                case Vector3D.Zplus:
                    z.add(-distance);
                    break;

                case Vector3D.Zsub:
                    z.add(distance);
                    break;
            }
        }

    }
}
