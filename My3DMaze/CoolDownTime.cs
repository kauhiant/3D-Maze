using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class CoolDownTime
    {
        private DateTime last;
        private TimeSpan ts;
        private double cdTime;

        public CoolDownTime(uint minisec = 0)
        {
            cdTime = minisec/1000.0;
        }

        public void setCoolDownTime(uint cd)
        {
            this.cdTime = cd;
        }

        public void record()
        {
            last = DateTime.Now;
        }

        public bool isCoolDown()
        {
            ts = DateTime.Now - last;
            return ts.TotalSeconds < cdTime;
        }
        
    }
}
