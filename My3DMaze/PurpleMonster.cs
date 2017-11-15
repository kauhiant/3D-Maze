using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class PurpleMonster:Monster
    {
        PurpleMonster(int x, int y, int z):base('P',x,y,z)
        {
            HP = 5;
            power = 2; attackRange = 2; trackRange = 8;
            bonus = 10;
        }

        //怪物成長
        public void growUp(Monster A)
        {
            addTrackRange(1);
            addBonus(A.getBonus());
            addPower(A.getPower());
            addHP(1);
        }
    }
}
