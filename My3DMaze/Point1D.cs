using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class Range1D
    {
        public int minValue { get; private set; }
        public int maxValue { get; private set; }
        public Range1D(int leftVal, int rightVal)
        {
            if (rightVal > leftVal)
            {
                int tmp = rightVal;
                rightVal = leftVal;
                leftVal = tmp;
            }
            this.minValue = leftVal;
            this.maxValue = rightVal;
        }

        public Range1D(int size)
        {
            this.minValue = 0;
            this.maxValue = size - 1;
        }
    }

    class Point1D
    {
        public int value{ get; private set; }
        public Point1D(int value = 0)
        {
            this.value = value;
        }

        public void add(int value)
        {
            this.value += value;
        }

        public int distanceTo(Point1D target)
        {
            return Math.Abs(this.value - target.value);
        }

        public bool inRange(int a,int b)
        {
            if (a == b)
            {
                if (value == a) return true;
                else return false;
            }
            else if (a < b)
            {
                int tmp = a;
                a = b;
                b = tmp;
            }
            return (value >= a && value <= b);
        }
        //不包括邊緣
        public bool inRange(Range1D range)
        {
            return (this.value > range.minValue && this.value < range.maxValue);
        }
        //在邊緣上
        public bool onEdge(Range1D range)
        {
            return (this.value == range.minValue || this.value == range.maxValue);
        }

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
