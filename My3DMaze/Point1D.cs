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

        // set min/maxValue by left/rightVal.
        public Range1D(int minVal, int maxVal)
        {
            if (minVal > maxVal)
            {
                int tmp = maxVal;
                maxVal = minVal;
                minVal = tmp;
            }
            this.minValue = minVal;
            this.maxValue = maxVal;
        }

        // set maxValue by size.
        public Range1D(int size)
        {
            this.minValue = 0;
            this.maxValue = size - 1;
        }

        public override string ToString()
        {
            return String.Format("[{0},{1}]", minValue, maxValue);
        }
    }

    class Point1D
    {
        public int value{ get; private set; }
        public Point1D(int value = 0)
        {
            this.value = value;
        }

        // set value by value.
        public void set(int value)
        {
            this.value = value;
        }

        // add val to value.
        public void add(int value)
        {
            this.value += value;
        }

        // return distance between this and target.
        public int distanceTo(Point1D target)
        {
            return Math.Abs(this.value - target.value);
        }

        // is this in the range[a,b]?
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

        // is this in the range?
        public bool inRange(Range1D range)
        {
            return (this.value >= range.minValue && this.value <= range.maxValue);
        }

        // is this on edge of the range?
        public bool onEdge(Range1D range)
        {
            return (this.value == range.minValue || this.value == range.maxValue);
        }

        // return value.
        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
