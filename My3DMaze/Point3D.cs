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
        public Point1D X { get; private set; }
        public Point1D Y { get; private set; }
        public Point1D Z { get; private set; }

        public int x { get { return X.value; } }
        public int y { get { return Y.value; } }
        public int z { get { return Z.value; } }

        public Point3D(int X,int Y,int Z)
        {
            this.X = new Point1D(X);
            this.Y = new Point1D(Y);
            this.Z = new Point1D(Z);
        }

        public Point3D copy()
        {
            Point3D retCopy = new Point3D(x,y,z);
            return retCopy;
        }

        public int valueAtPlane(Plane plane)
        {
            switch (plane)
            {
                case Plane.X:
                    return x;
                case Plane.Y:
                    return y;
                case Plane.Z:
                    return z;
                default:
                    return -1;
            }
        }

        public int distanceTo(Point3D target)
        {
            int tmp = 0;
            int min = int.MaxValue;
            tmp = this.X.distanceTo(target.X) ;
            min = min < tmp ? min : tmp;
            tmp = this.Y.distanceTo(target.Y);
            min = min < tmp ? min : tmp;
            tmp = this.Z.distanceTo(target.Z);
            min = min < tmp ? min : tmp;

            return min;
        }

        public override string ToString()
        {
            return "x=" + X + " ,y=" + Y + " ,z=" + Z;
        }
        //不包刮邊緣
        public bool inRange(Range3D range)
        {
            return (this.X.inRange(range.xRange) &&
                this.Y.inRange(range.yRange) &&
                this.Z.inRange(range.zRange));
        }

        public bool onEdge(Range3D range)
        {
            return (
                X.onEdge(range.xRange) && Y.inRange(range.yRange) && Z.inRange(range.zRange) ||
                Y.onEdge(range.xRange) && X.inRange(range.yRange) && Z.inRange(range.zRange) ||
                Z.onEdge(range.xRange) && X.inRange(range.yRange) && Y.inRange(range.zRange)
                );
        }

        public void moveForward(Vector3D vector ,int distance)
        {
            switch (vector)
            {
                case Vector3D.Xplus:
                    X.add(distance);
                    break;

                case Vector3D.Xsub:
                    X.add(-distance);
                    break;

                case Vector3D.Yplus:
                    Y.add(distance);
                    break;

                case Vector3D.Ysub:
                    Y.add(-distance);
                    break;

                case Vector3D.Zplus:
                    Z.add(distance);
                    break;

                case Vector3D.Zsub:
                    Z.add(-distance);
                    break;
            }
        }
        

    }
}
