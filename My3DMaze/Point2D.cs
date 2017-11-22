using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    public enum Plane { X, Y, Z }
    public enum Vector2D { Up, Down, Left, Right , Null}

    class Range2D
    {
        public Range1D xRange { get; private set; }
        public Range1D yRange { get; private set; }
        public Range2D(Range1D xRange, Range1D yRange)
        {
            this.xRange = xRange;
            this.yRange = yRange;
        }
    }
    
    class Point2D
    {
        private Point1D X;
        private Point1D Y;/*{ get; private set; }*/
        
        public int x { get { return X.value; } }
        public int y { get { return Y.value; } }

        private Point3D binded;
        public Plane plane { get; private set; }
        public int planeValue
        {
            get
            {
                switch (plane)
                {
                    case Plane.X:
                        return binded.X;
                    case Plane.Y:
                        return binded.Y;
                    case Plane.Z:
                        return binded.Z;
                    default:
                        return -1;
                }
            }
        }

        public Point2D(int X, int Y)
        {
            this.X = new Point1D(X);
            this.Y = new Point1D(Y);
        }

        public Point2D(Point1D X , Point1D Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void bind(Point1D X, Point1D Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void bindWith(Point3D target , Plane plane)
        {
            this.binded = target;
            this.plane = plane;
        }

        public void moveForward(Vector2D vector , int distance=1)
        {
            switch (vector)
            {
                case Vector2D.Up:
                    this.Y.add(-distance);
                    break;

                case Vector2D.Down:
                    this.Y.add(distance);
                    break;

                case Vector2D.Left:
                    this.X.add(-distance);
                    break;

                case Vector2D.Right:
                    this.X.add(distance);
                    break;
            }
        }

        public void moveBack(Vector2D vector, int distance = 1)
        {
            switch (vector)
            {
                case Vector2D.Up:
                    this.Y.add(distance);
                    break;

                case Vector2D.Down:
                    this.Y.add(-distance);
                    break;

                case Vector2D.Left:
                    this.X.add(distance);
                    break;

                case Vector2D.Right:
                    this.X.add(-distance);
                    break;
            }
        }

        public bool inRange(Range2D range)
        {
            return (this.X.inRange(range.xRange) && this.Y.inRange(range.yRange));
        }


        public override string ToString()
        {
            return "X = " + x + " ,Y = " + y ;
        }
    }
}
