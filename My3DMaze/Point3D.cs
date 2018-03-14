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

        // set 3 proterties by 3 arguments.
        public Range3D(Range1D xRange, Range1D yRange, Range1D zRange)
        {
            this.setRange3D(xRange, yRange, zRange);
        }

        private void setRange3D(Range1D xRange, Range1D yRange, Range1D zRange)
        {
            this.xRange = xRange;
            this.yRange = yRange;
            this.zRange = zRange;
        }
    }

    class Point3D
    {
        private Random rand = new Random();

        //軸
        public Point1D X { get; private set; }
        public Point1D Y { get; private set; }
        public Point1D Z { get; private set; }

        public int x { get { return X.value; } }
        public int y { get { return Y.value; } }
        public int z { get { return Z.value; } }

        // set 3 properties by 3 arguments.
        public Point3D(int X,int Y,int Z)
        {
            this.X = new Point1D(X);
            this.Y = new Point1D(Y);
            this.Z = new Point1D(Z);
        }

        // return a copy of this Point-3D.
        public Point3D copy()
        {
            Point3D retCopy = new Point3D(x,y,z);
            return retCopy;
        }

        // get a point-2D on the plane.
        public Point2D get2DPointOnPlane(Dimension plane)
        {
            Point2D point2D = new Point2D(this, plane);
            return point2D;
        }

        // get value of the plane.
        public int valueAtDimension(Dimension dimension)
        {
            switch (dimension)
            {
                case Dimension.X:
                    return X.value;
                case Dimension.Y:
                    return Y.value;
                case Dimension.Z:
                    return Z.value;
                default:
                    return -1;
            }
        }

        // get max distance of 3 axis
        // 3個軸分開算，取最大的.
        public int distanceTo(Point3D target)
        {
            int tmp = 0;
            int max = int.MinValue;

            tmp = this.X.distanceTo(target.X) ;
            max = max > tmp ? max : tmp;

            tmp = this.Y.distanceTo(target.Y);
            max = max > tmp ? max : tmp;

            tmp = this.Z.distanceTo(target.Z);
            max = max > tmp ? max : tmp;

            return max;
        }

        // get value.
        public override string ToString()
        {
            return "x=" + X + " ,y=" + Y + " ,z=" + Z;
        }

        // is in the range?
        public bool inRange(Range3D range)
        {
            return (this.X.inRange(range.xRange) &&
                this.Y.inRange(range.yRange) &&
                this.Z.inRange(range.zRange));
        }

        // is on edge?
        public bool onEdge(Range3D edge)
        {
            return (
                X.onEdge(edge.xRange) && Y.inRange(edge.yRange) && Z.inRange(edge.zRange) ||
                Y.onEdge(edge.xRange) && X.inRange(edge.yRange) && Z.inRange(edge.zRange) ||
                Z.onEdge(edge.xRange) && X.inRange(edge.yRange) && Y.inRange(edge.zRange)
                );
        }

        // move distance forward the vector.
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
                default:
                    break;
            }
        }
   
        public static Point3D createPointOnPlane(Plane plane)
        {
            Point3D point = new Point3D(plane.value, plane.value, plane.value);
            Point2D temp = new Point2D(point, plane.dimension);
            temp.moveTo(0, 0);
            return point;
        }

        public void set(int x,int y,int z)
        {
            this.X.set(x);
            this.Y.set(y);
            this.Z.set(z);
        }

        public void moveTo(Point3D target)
        {
            this.set(target.x, target.y, target.z);
        }

        public void moveRandom(int times=1)
        {
            while(times-- > 0)
            {
                int randNum = rand.Next(7);
                switch (randNum)
                {
                    case 0:
                        X.add(1);
                        break;

                    case 1:
                        X.add(-1);
                        break;

                    case 2:
                        Y.add(1);
                        break;

                    case 3:
                        Y.add(-1);
                        break;

                    case 4:
                        Z.add(1);
                        break;

                    case 5:
                        Z.add(-1);
                        break;
                    default:
                        break;
                }
            }
        }

        public bool Equals(Point3D obj)
        {
            return (x == obj.x && y == obj.y && z == obj.z);
        }
    }
}
