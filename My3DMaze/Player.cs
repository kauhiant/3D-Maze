using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class Player
    {
        private const int energyPow = 1;    //energy 和 HP 的比例
        private Random rand = new Random();

        private Map3D map;
        private Map2D map2d;
        
        public Color color  { get; private set; }   //顏色
        public Image shape  { get; private set; }   //外觀

        public int score    { get; private set; }   //分數
        public int HP       { get; private set; }   //血量
        public int power    { get; private set; }   //力量
        public int energy   { get; private set; }   //能量
        public int seenSize { get; private set; }   //視野

        public Point3D location   { get; private set; }   //座標 你的位置
        public Point2D location2d { get; private set; }

        public Plane plane { get { return location2d.plane; }  }   //你的視角在哪個平面 "x" or "y" or "z"
        public int   X     { get { return location.x; } }
        public int   Y     { get { return location.y; } }
        public int   Z     { get { return location.z; } }

        //初始化 
        public Player(Map3D maze, Point3D location, Dimension p=Dimension.Z,int hp=15, int seenSize = 7)
        {
            this.location   = location;
            this.location2d = location.get2DPointOnPlane(p);

            this.HP       = hp;
            this.energy   = HP * energyPow; //初始能量和血量的關係
            this.power    = 1;
            this.score    = 0;
            this.seenSize = seenSize;

            this.color = Color.Green;
            this.shape = Image.FromFile(@"./Shield.png");

            this.map   = maze;
            this.map2d = map.creat2DMapOn(location2d.plane);
        }



        //初始化 [remove this constructor when remake is complete]
        public Player(int x = 0, int y = 0, int z = 0, Dimension p = Dimension.Z, int hp = 15)
        {
            score = 0;
            location = new Point3D(x, y, z);
            HP = hp;
            energy = HP * energyPow; //初始能量和血量的關係
            power = 1;
            this.seenSize = seenSize;
            map.beAttack(location, -1);
        }

        // [remove this constructor when remake is complete]
        public int getPlaneValue()
        {
            return this.location2d.plane.value;
        }

        //在視角平面上的二維座標
        // [remove this constructor when remake is complete]
        public int getA()
        {
            return location2d.x;
        }
        public int getB()
        {
            return location2d.y;
        }

        //輸出位置字串[remove this constructor when remake is complete]
        public string showPoint()
        {
            return location.ToString();
        }
   
        public void addSeenSize(int val = 1)
        {
            this.seenSize += val;
            if (seenSize < 0)
                seenSize = 0;
        }
             
        //增加或減少能量
        public void addEnergy(int tmp)
        {
            energy += tmp;
            //能量不能大於生命
            if (energy > HP * energyPow)
                energy = HP * energyPow;
            //能量不能小於0
            if (energy < 0)
                energy = 0;
        }

        //增加或減少生命
        public void addHP(int tmp)
        {
            HP += tmp;
            if (HP < 0) HP = 0;
        }

        //增加或減少力量
        public void addPower(int tmp)
        {
            power += tmp;
        }

        //增加或減少分數
        public void addScore(int tmp)
        {
            score += tmp;
        }

        //消耗能量換取生命
        public void EnergyConvertHP()
        {
            if (energy > HP / 2)
            {
                HP += energy/4;
            }
            energy /= 2;
        }

        //獲取獎勵 [力量+1]:10%   [生命+1]:40%  [能量+1]:50%
        public void getBonus(int times=1)
        {
            for (; times>0; --times)
            {
                double tmp = rand.NextDouble();
                if (tmp < 0.1)
                    power++;
                else if (tmp < 0.5)
                    HP++;
                else
                    energy++;
            }
        }


        // [remove this constructor when remake is complete] //
        //依字串決定要往哪個方向移動一步 在地圖上
        public bool move(Vector2D udlr, ref int[,,] map, ref int[,] nmap, int map_size)
        {
            mmove(location2d, udlr, ref nmap);

            //是否走到邊界
            return location.onEdge(Const.mazeRange);
        }
        //二維視角移動 (a,b) a:往左方向 b:往下方向
        private bool mmove(Point2D location2D,Vector2D udlr,ref int[,] nmap)
        {
            location2D.moveForward(udlr);
            if (nmap[location2D.x, location2D.y] == 0)
                return true;
            else
            {
                location2D.moveForward(udlr, -1);
                return false;
            }
        }

        // [remove this constructor when remake is complete] //
        //Turn Plane 轉移視角
        private int planeNumber = 2; //0:X平面 1:Y平面 2:Z平面
        public void turn(string p, ref int[,,] map)
        {
            if (p == "down")    //往下旋轉
            {
                planeNumber++;
                if (planeNumber > 2) planeNumber = 0;
            }
            else if (p == "right")  //往右旋轉
            {
                planeNumber--;
                if (planeNumber < 0) planeNumber = 2;
            }
        }

        // [remove this constructor when remake is complete] //
        //攻擊地圖中的牆壁 全範圍 距離1 加自己共27格
        public void attack(ref int[,,] map)
        {
            //能量不夠不能攻擊
            if (energy == 0) return;
            
            energy --;
            //因為走到邊界遊戲就結束了沒辦法攻擊邊界外的空資料所以不設邊界條件
            //以後更新可能要加邊界條件
            for (int i = location.x - 1; i < location.x + 2; ++i)
                for (int j = location.y - 1; j < location.y + 2; ++j)
                    for (int k = location.z - 1; k < location.z + 2; ++k)
                    {
                        map[i, j, k] -= power;  //攻擊牆壁
                        if (map[i, j, k] < 0) map[i, j, k] = 0; //如果牆壁生命小於0 那就歸零
                    }
                    
        }

        // move
        public void moveForward(Vector2D vector)
        {
            Point3D target = this.location.copy();
            Point2D target2D = target.get2DPointOnPlane(plane.dimension);

            target2D.moveForward(vector, 1);

            if (map.valueAt(target) != 0)
                return;

            map.setValueAt(location, 0);
            map.setValueAt(target, -1);
            this.location.moveTo(target);
        }
        
        //攻擊地圖中的牆壁 全範圍 距離1 加自己共27格
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
                        map.beAttack(attackTarget, power);
                    }
        }

        // attack to monster and grid of map
        public void attack()
        {
            if (energy == 0) return;
            this.addEnergy(-1);
            this.attack(this.map);
            foreach(var monster in MonsterController.monsterList)
            {
                this.attack(monster);
            }
        }

        // attack to monster
        private void attack(Monster target)
        {
            if (this.location.distanceTo(target.location) > 1)
                return;

            target.addHP(-this.power);
            if (target.isDead())
                this.getBonus(target.KillMonster());
        }
        
        // show on MapGraph
        public void showOn(MapGraph target)
        {
            target.drawGrid(this.seenSize, this.seenSize, this.color, (this.HP<126)?this.HP*255/126:255);
            target.draw2DMap(this.seenSize, this.seenSize, this.shape);
        }

        // return this information
        public string information()
        {
            string retValue = "";
            retValue += "HP:" + this.HP + Environment.NewLine;
            retValue += "power:" + this.power + Environment.NewLine;
            retValue += "energy:" + this.energy + Environment.NewLine;
            retValue += "Plane:" + this.plane.ToString();
            return retValue;
        }

        // seen range.
        public Range2D seenRange()
        {
            return new Range2D(this.location2d, this.seenSize);
        }
    }
}
