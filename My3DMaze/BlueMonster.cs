using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class BlueMonster:Monster
    {
        BlueMonster(int x, int y, int z):base('B',x,y,z)
        {
            HP = 1;
            power = 2; attackRange = 3; trackRange = 0;
            bonus = 10;
        }
        /*
        public override void move(int[,,] map, Player A)
        {
            if (trackRange == 0)
                return;//如果不會追蹤(移動)
            //base.move(map, A);
        }*/
    }
}
