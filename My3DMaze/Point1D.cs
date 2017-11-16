using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
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

        public override string ToString()
        {
            return this.value.ToString();
        }
    }
}
