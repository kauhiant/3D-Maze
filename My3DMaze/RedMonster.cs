using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class RedMonster:Monster
    {
        public RedMonster(Point3D location, Map3D map)
            : base(location, map)
        {
            initProperty(10, 1, 1, 5);
            initShape(Color.Red, null);
        }

       
    }
}
