using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class BlueMonster:Monster
    {
        public BlueMonster(Point3D location,Map3D map)
            :base(location,map)
        {
            initProperty(10, 2, 3, 0,10);
            initShape(Color.Blue, null);
        }
        
        public override void move(Player target)
        {
            // 不會移動.
        }
    }
}
