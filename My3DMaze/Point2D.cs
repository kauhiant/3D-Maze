using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    public enum Dimension { X, Y, Z }
    public enum Vector2D { Up, Down, Left, Right , Null}

    class Plane
    {
        public Dimension dimension;
        public int value;

        public Plane(Dimension dimension, int value)
        {
            this.dimension = dimension;
            this.value = value;
        }

        public bool onPlane(Plane plane, int planeFix = 0)
        {
            return plane.dimension == this.dimension
                && plane.value == this.value + planeFix;
        }

        public override string ToString()
        {
            return String.Format(" Plane {0} = {1}", dimension, value);
        }
    }

    class Range2D
    {
        public Range1D xRange { get; private set; }
        public Range1D yRange { get; private set; }

        // set 2 proterties by 2 arguments.
        public Range2D(Range1D xRange, Range1D yRange)
        {
            this.xRange = xRange;
            this.yRange = yRange;
        }

        // the range expand from center.
        public Range2D(Point2D center, int extraRange)
        {
            this.xRange = new Range1D(center.x - extraRange, center.x + extraRange);
            this.yRange = new Range1D(center.y - extraRange, center.y + extraRange);
        }

        public override string ToString()
        {
            return  String.Format("X:{0}", xRange.ToString()) + Environment.NewLine +
                    String.Format("Y:{0}", yRange.ToString());
        }
    }
    
    class Point2D
    {
// 屬性
        private Point1D X;
        private Point1D Y;
        private Point3D binded;

        public int x { get { return X.value; } }
        public int y { get { return Y.value; } }
        public Plane plane { get; private set; }


// 建構子
        // 2D-Point is on the plane of binded 3D-Point.
        public Point2D(Point3D binded , Dimension dimension = Dimension.Z)
        {
            this.plane = new Plane(dimension, 0);
            this.bindWith(binded, dimension);
        }
        

//方法
        private void bind(Point1D X, Point1D Y)
        {
            this.X = X;
            this.Y = Y;
        }

        private void bindWith(Point3D target , Dimension dimension)
        {
            this.binded = target;
            this.changePlane(dimension);
        }

        // change 2D-Point on the plane of binde-3D-Point.
        public void changePlane(Dimension dimension)
        {
            this.plane.dimension = dimension;
            switch (dimension)
            {
                case Dimension.X:
                    this.plane.value = binded.x;
                    this.bind(binded.Y, binded.Z);
                    break;
                case Dimension.Y:
                    this.plane.value = binded.y;
                    this.bind(binded.Z, binded.X);
                    break;
                case Dimension.Z:
                    this.plane.value = binded.z;
                    this.bind(binded.X, binded.Y);
                    break;
            }
        }

        // move point-2D to target.
        public void moveTo(Point2D target)
        {
            this.X.set(target.x);
            this.Y.set(target.y);
        }

        // move Point-2D to Point2D(x,y).
        public void moveTo(int x, int y)
        {
            this.X.set(x);
            this.Y.set(y);
        }

        // // move distance forward the vector.
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

        // is this 2D-point in the range?
        public bool inRange(Range2D range)
        {
            return (this.X.inRange(range.xRange) 
                 && this.Y.inRange(range.yRange));
        }

        // is this 2D-point on the edge?
        public bool onEdge(Range2D edge)
        {
            return  
                (X.onEdge(edge.xRange) && Y.inRange(edge.yRange)) || 
                (Y.onEdge(edge.yRange) && X.inRange(edge.xRange));
        }

        // return "( $x , $y )".
        public override string ToString()
        {
            return String.Format("( {0} , {1} )", x, y);
        }
        
    }
}
