using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class Player
    {
        Random rand = new Random();
        const int energyPow = 1;    //energy 和 HP 的比例

        int healthPoint, power, energy; //血量、力量、能量
        string plane;   //你的視角在哪個平面 "x" or "y" or "z"
        //int x, y, z;    //座標 你的位置
        public Point3D location { get; private set; }
        int score;      //分數

        //初始化 
        public Player(int x=0,int y=0,int z=0,string p="z",int hp=15)
        {
            score = 0;
            location = new Point3D(x, y, z);
            plane = p;
            healthPoint = hp;
            energy = healthPoint*energyPow; //初始能量和血量的關係
            power = 1;
        }

        
        public int getPower()   {   return power;   }
        public int getHP() { return healthPoint; }
        public int getEnergy() { return energy; }
        public int getScore() { return score; }

        public string getPlane() { return plane; }
        public int X { get { return location.x; } }
        public int Y { get { return location.y; } }
        public int Z { get { return location.z; } }

        //在哪個平面
        public int get(string c)
        {
            switch (c)
            {
                case "x":
                case "X":
                    return location.x;
                case "y":
                case "Y":
                    return location.y;
                case "z":
                case "Z":
                    return location.z;
                default:
                    return -1;
            }
        }

        //在視角平面上的二維座標
        public int getA()
        {
            if (plane == "x") return location.y;
            else if (plane == "y") return location.z;
            else if (plane == "z") return location.x;
            else return 0;
        }
        public int getB()
        {
            if (plane == "x") return location.z;
            else if (plane == "y") return location.x;
            else if (plane == "z") return location.y;
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
            if (energy < healthPoint * energyPow)
                energy += tmp;
            if (energy > healthPoint * energyPow)
                energy = healthPoint * energyPow;
        }

        //增加或減少生命
        public void addHP(int tmp)
        {
            healthPoint += tmp;
            if (healthPoint < 0) healthPoint = 0;
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
            if (energy > healthPoint / 2)
            {
                healthPoint += energy/4;
            }
            energy /= 2;
        }

        //依字串決定要往哪個方向移動一步 在地圖上
        public bool move(string udlr, ref int[,,] map, ref int[,] nmap, int map_size)
        {
            Point2D point2D = new Point2D(0, 0);
            switch (plane)
            {
                
                case "x":
                    point2D.set(Y, Z);
                    mmove(point2D, udlr, ref nmap);
                    this.location.setPoint(X , point2D.X , point2D.Y);
                    break;
                case "y":
                    point2D.set(Z, X);
                    mmove(point2D, udlr, ref nmap);
                    this.location.setPoint(point2D.Y , Y , point2D.X);
                    break;
                case "z":
                    point2D.set(X, Y);
                    mmove(point2D, udlr, ref nmap);
                    this.location.setPoint(point2D.X , point2D.Y , Z);
                    break;
            }
            //是否走到邊界
            return
                (X == 0 || Y == 0 || Z == 0 || X == map_size - 1 || Y == map_size - 1 || Z == map_size - 1);
        }
        //二維視角移動 (a,b) a:往左方向 b:往下方向
        private bool mmove(Point2D location2D,string udlr,ref int[,] nmap)
        {
            switch (udlr)
            {
                case "up":
                    if (nmap[location2D.X, location2D.Y - 1] == 0)    //判斷這一格能不能走 是牆壁還是路 能走就回傳TRUE
                    {
                        location2D.Y -= 1; return true;
                    }
                    break;
                case "down":
                    if (nmap[location2D.X, location2D.Y + 1] == 0)
                    {
                        location2D.Y += 1; return true;
                    }
                    break;
                case "left":
                    if (nmap[location2D.X - 1, location2D.Y] == 0)
                    {
                        location2D.X -= 1; return true;
                    }  
                    break;
                case "right":
                    if (nmap[location2D.X + 1, location2D.Y] == 0)
                    {
                        location2D.X += 1; return true;
                    }
                    break;
            }
            return false;
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
                case 0: plane = "x"; break;
                case 1: plane = "y"; break;
                case 2: plane = "z"; break;
            }
        }

        //攻擊地圖中的牆壁 全範圍 距離1 加自己共27格
        public void attack(ref int[,,] map) {
            if (energy > 0)
            {
                energy --;
                //因為走到邊界遊戲就結束了沒辦法攻擊邊界外的空資料所以不設邊界條件
                //以後更新可能要加邊界條件
                for (int i = X - 1; i < X + 2; i++)
                {
                    for (int j = Y - 1; j < Y + 2; j++)
                    {
                        for (int k = Z - 1; k < Z + 2; k++)
                        {
                            map[i, j, k] -= power;  //攻擊牆壁
                            if (map[i, j, k] < 0) map[i, j, k] = 0; //如果牆壁生命小於0 那就歸零
                        }
                    }
                }
            }
        }
        
        //獲取獎勵 [力量+1]:10%   [生命+1]:40%  [能量+1]:50%
        public void getBonus(double tmp)
        {
            if (tmp < 0.1)
                power++;
            else if (tmp < 0.5)
                healthPoint++;
            else if (tmp < 1)
                energy++;
        }
    }
}
