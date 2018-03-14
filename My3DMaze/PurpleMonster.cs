using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class PurpleMonster:Monster
    {
        public PurpleMonster(Point3D location,Map3D map)
            :base(location,map)
        {
            initProperty(5, 2, 2, 8,10);
            initShape(Color.Purple, Image.FromFile(@"./Angry.png"));
            Console.WriteLine("Avenger create!!!");
        }

        //攻擊地圖中的牆壁 全範圍 距離1 不加自己共26格
        private void attack(Map3D map)
        {
            Point3D attackTarget = this.location.copy();

            for (int i = location.x - 1; i < location.x + 2; ++i)
                for (int j = location.y - 1; j < location.y + 2; ++j)
                    for (int k = location.z - 1; k < location.z + 2; ++k)
                    {
                        attackTarget.set(i, j, k);
                        if (attackTarget.Equals(location))
                            continue;
                        if (map.valueAt(attackTarget) <= 0)
                            continue;
                        map.beAttack(attackTarget, power / 10 + 1);
                    }
        }

        public override bool attack(Player target)
        {
            attack(map);
            return base.attack(target);
        }

    }
}
