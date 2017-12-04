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
        public Range2D(Point2D center, int extraRange)
        {
            this.xRange = new Range1D(center.x - extraRange, center.x + extraRange);
            this.yRange = new Range1D(center.y - extraRange, center.y + extraRange);
        }
    }
    
    class Point2D
    {
// 屬性
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
                        return binded.x;
                    case Plane.Y:
                        return binded.y;
                    case Plane.Z:
                        return binded.z;
                    default:
                        return -1;
                }
            }
        }


// 建構子
        public Point2D(int x, int y)
        {
            this.bind(new Point1D(x), new Point1D(y));
        }

        public Point2D(Point1D X , Point1D Y)
        {
            this.bind(X, Y);
        }

        public Point2D(Point3D binded , Plane plane)
        {
            this.binded = binded;
            this.plane = plane;
            this.changePlane(plane);
        }
        

//方法
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

        public void changePlane(Plane plane)
        {
            this.plane = plane;
            switch (plane)
            {
                case Plane.X:
                    this.bind(binded.Y, binded.Z);
                    break;
                case Plane.Y:
                    this.bind(binded.Z, binded.X);
                    break;
                case Plane.Z:
                    this.bind(binded.X, binded.Y);
                    break;
            }
        }
        
        public void moveTo(Point2D target)
        {
            this.X.set(target.x);
            this.Y.set(target.y);
        }

        public void moveTo(int x, int y)
        {
            this.X.set(x);
            this.Y.set(y);
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

       

        public bool inRange(Range2D range)
        {
            return (this.X.inRange(range.xRange) && this.Y.inRange(range.yRange));
        }

        public bool onEdge(Range2D range)
        {
            return  
                (X.onEdge(range.xRange) && Y.inRange(range.yRange)) || 
                (Y.onEdge(range.yRange) && X.inRange(range.xRange));
        }

//複寫
        public override string ToString()
        {
            return "X = " + x + " ,Y = " + y ;
        }
    }
}
