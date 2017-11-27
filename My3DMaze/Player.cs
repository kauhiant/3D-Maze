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
        private Map3D map;
        private Map2D map2d;
        public Point2D location2d;

        Random rand = new Random();
        const int energyPow = 1;    //energy 和 HP 的比例
        public int score        { get; private set; }   //分數
        public Graphics image   { get; private set; }   //外觀

        public int HP       { get; private set; }   //血量
        public int power    { get; private set; }   //力量
        public int energy   { get; private set; }   //能量

        public Plane plane      { get; private set; }   //你的視角在哪個平面 "x" or "y" or "z"
        public Point3D location { get; private set; }   //座標 你的位置
        public int X { get { return location.x; } }
        public int Y { get { return location.y; } }
        public int Z { get { return location.z; } }

        //初始化 
        public Player(Map3D maze, int x=0,int y=0,int z=0,Plane p=Plane.Z,int hp=15)
        {
            score = 0;
            location = new Point3D(x, y, z);
            location2d = new Point2D(location,p);
            plane = p;
            HP = hp;
            energy = HP * energyPow; //初始能量和血量的關係
            power = 1;

            this.map = maze;
            this.map2d = new Map2D(this.map);
            this.map2d.creat2DMapOn(p, location.valueAtPlane(p));
        }
        //初始化 
        public Player(int x = 0, int y = 0, int z = 0, Plane p = Plane.Z, int hp = 15)
        {
            score = 0;
            location = new Point3D(x, y, z);
            plane = p;
            HP = hp;
            energy = HP * energyPow; //初始能量和血量的關係
            power = 1;
        }

        //在哪個平面
        public string getPlaneString()
        {
            switch (this.plane)
            {
                case Plane.X:
                    return "X = " + location.X;
                case Plane.Y:
                    return "Y = " + location.Y;
                case Plane.Z:
                    return "Z = " + location.Z;
                default:
                    return "Null";
            }
        }
        public int getPlaneValue()
        {
            switch (this.plane)
            {
                case Plane.X:
                    return location.x;
                case Plane.Y:
                    return location.y;
                case Plane.Z:
                    return location.z;
                default:
                    return -1;
            }
        }

        //在視角平面上的二維座標
        public int getA()
        {
            if (plane == Plane.X) return location.y;
            else if (plane == Plane.Y) return location.z;
            else if (plane == Plane.Z) return location.x;
            else return 0;
        }
        public int getB()
        {
            if (plane == Plane.X) return location.z;
            else if (plane == Plane.Y) return location.x;
            else if (plane == Plane.Z) return location.y;
            else return 0;
        }
        
        //輸出位置字串
        public string showPoint()
        {
            return location.ToString();
        }
        
        //增加或減少能量
        public void addEnergy(int tmp)
        {
            if (energy < HP * energyPow)
                energy += tmp;
            //能量不能大於生命
            if (energy > HP * energyPow)
                energy = HP * energyPow;
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

        //依字串決定要往哪個方向移動一步 在地圖上
        public bool move(Vector2D udlr, ref int[,,] map, ref int[,] nmap, int map_size)
        {
            Point2D point2D = new Point2D(0, 0);
            switch (plane)
            {
                case Plane.X:
                    point2D.bind(location.Y, location.Z);
                    mmove(point2D, udlr, ref nmap);
                    break;
                case Plane.Y:
                    point2D.bind(location.Z, location.X);
                    mmove(point2D, udlr, ref nmap);
                    break;
                case Plane.Z:
                    point2D.bind(location.X, location.Y);
                    mmove(point2D, udlr, ref nmap);
                    break;
            }
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
                location2D.moveBack(udlr);
                return false;
            }
        }
        

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
            switch (planeNumber)   
            {
                case 0: plane = Plane.X; break;
                case 1: plane = Plane.Y; break;
                case 2: plane = Plane.Z; break;
            }
        }

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
        
        //獲取獎勵 [力量+1]:10%   [生命+1]:40%  [能量+1]:50%
        public void getBonus(double tmp)
        {
            if (tmp < 0.1)
                power++;
            else if (tmp < 0.5)
                HP++;
            else
                energy++;
        }
        
        public void moveForward(Vector2D forward)
        {
            this.location2d.moveForward(forward,1);

            if (map.valueAt(location) != 0)
                this.location2d.moveBack(forward, 1);
        }

        public void showOn(MapGraph target)
        {

        }
    }
}
