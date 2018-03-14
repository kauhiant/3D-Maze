using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class MonsterController:Monster
    {
        private static Random rand = new Random();
        private static PurpleMonster avenger = null;
        private static int redMonsterNumber  = 0;
        private static int blueMonsterNumber = 0;
        private static int controllerNumber  = 0;

        public static List<Monster> monsterList = new List<Monster>();  
        public static Player enemy;
        public static int size{ get { return monsterList.Count; } }
        public static MapGraph scene;

        private int age;
        

        public MonsterController(Point3D location, Map3D map)
            :base(location,map)
        {
            initProperty(100, 0, 10, 0, 100);
            initShape(Color.Black, Image.FromFile(@"./Angry.png"));
            age = 1;
            MonsterController.monsterList.Add(this);
            controllerNumber++;
        }

        public static void Initializer(Player enemy, MapGraph scene)
        {
            MonsterController.enemy = enemy;
            MonsterController.scene = scene;
        }

        public void createMonster()
        {
            Point3D locate = location.copy();
            locate.moveRandom(age);
            if (map.valueAt(locate) != 0) return;

            Monster obj = null;
            var rate = rand.NextDouble() * age;

            if(rate > 9.0)
            {
                obj = new BlueMonster(locate, map);
                blueMonsterNumber++;
            }
            else if (rate > 2.0)
            {
                obj = new RedMonster(locate, map);
                redMonsterNumber++;
            }

            if(obj != null)
                monsterList.Add(obj);
        }

        public static void action()
        {
            for (var i = 0; i < size ; ++i)
            {
                Monster monster = monsterList.ElementAt(i);
                if (monster.isDead())
                {
                    monsterDead(monster);
                    if (size == 0) return;
                    continue;
                }
                if (monster is MonsterController)
                {
                    MonsterController target = monster as MonsterController;
                    target.ageExpend();
                    target.createMonster();
                }
                monster.move(enemy);
                monster.attack(enemy);
            }

            map.setValueAt(enemy.location, -1);
            foreach (var monster in monsterList)
            {
                map.setValueAt(monster.location, -1);
            }
        }
        

        public static void show()
        {
            foreach(var monster in monsterList)
            {
                drawMonster(monster);
            }
        }

        public string information()
        {
            return String.Format("Age : {0} Count : {1}", age, size)+Environment.NewLine+
                String.Format("Location : ({0})",location.ToString())+Environment.NewLine+
                string.Format("HP : {0}",HP);
        }


        private static void drawMonster(Monster target)
        {
            Point2D locate = target.location.get2DPointOnPlane(enemy.plane.dimension);
            if (!locate.plane.onPlane(enemy.plane)) return;
            if (!locate.inRange(enemy.seenRange())) return;

            int x = locate.x - enemy.location2d.x;
            int y = locate.y - enemy.location2d.y;

            target.showOn(scene, enemy.seenSize + x, enemy.seenSize + y);
        }

        private static void monsterDead(Monster monster)
        {
            monsterList.Remove(monster);
            map.setValueAt(monster.location, 0);
            if (avenger == null)
            {
                monster = selectMonster();
                avenger = new PurpleMonster(monster.location, map);
                monsterDead(monster);
                monsterList.Add(avenger);
            }

            if (monster is RedMonster)
            {
                redMonsterNumber--;
                avenger.growUp(monster);
            }
            else if (monster is BlueMonster)
            {
                blueMonsterNumber--;
                avenger.growUp(monster);
            }
            else if (monster is PurpleMonster)
            {
                monster = selectMonster();
                if (monster == null)
                {
                    avenger = null;
                    return;
                } 
                avenger = new PurpleMonster(monster.location, map);
                monsterDead(monster);
                monsterList.Add(avenger);
            }
            else if (monster is MonsterController)
            {
                controllerNumber--;
            }
        }

        private static Monster selectMonster()
        {
            if (size == controllerNumber) return null;

            foreach (var target in monsterList)
            {
                if (target is MonsterController)
                    continue;
                return target;
            }

            return null;
        }

        private void ageExpend()
        {
            var rate = rand.NextDouble();
            if (rate < 0.1/age)
                this.age++;
            if (age > 10)
                age = 10;
        }
        
    }
}
