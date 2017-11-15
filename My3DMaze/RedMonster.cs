using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class RedMonster:Monster
    {
        RedMonster(int x,int y, int z):base('R',x,y,z)
        {
            HP = 5;
            power = 2; attackRange = 1; trackRange = 8;
            bonus = 1;
        }
    }
}
