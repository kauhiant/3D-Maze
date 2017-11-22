using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class Monster
    {
        protected Point3D location;//位置
        protected int x, y, z;    //位置
        protected int HP, power, attackRange, bonus;  //生命、力量、攻擊範圍、獎勵
        char type;  //型態 { R:紅怪物 B:藍怪物 P:復仇者 }
        protected int trackRange; //追蹤範圍
        Random rand = new Random(); //亂數產生器

        //初始化 T:怪物型態 [X,Y,Z]:在地圖上的位置
        //第二代要做很多種怪物!!
        public Monster(char T,int X,int Y,int Z)
        {
            if(T=='B' || T == 'b')
            {
                HP = 1;
                type = 'B';
            }
            else if(T == 'R' || T == 'r')
            {
                HP = 5;
                type = 'R';
            }
            else if(T == 'P' || T == 'p')
            {
                HP = 5;
                type = 'P';
            }
            else { HP = 0;type = 'D';  }
            x = X;  y = Y;  z = Z;
            location = new Point3D(X,Y,Z);
            changeType(T);
        }

        //殺死怪物 並回傳獎勵
        public int KillMonster()
        {
            HP = 0; power = 0; attackRange = 0; trackRange = 0;
            type = 'D';
            x = 0; y = 0; z = 0;
            //location destructure
            return bonus;
        }

        public int getX() { return x; }
        public int getY() { return y; }
        public int getZ() { return z; }

        public int X { get { return location.X; } }
        public int Y { get { return location.Y; } }
        public int Z { get { return location.Z; } }

        public char getType() { return type; }
        public int getHP() { return HP; }
        public int getPower() { return power; }
        public int getBonus() { return bonus; }

        public bool isDead() { return (HP == 0); }
        
        public void addTrackRange(int r) { trackRange += r; }
        public void addPower(int n) { power += n; }
        public void addBonus(int n) { bonus += n; }
        public void addHP(int n) {
            HP += n;
            if (HP < 0) //如果HP低於零就歸零
                HP = 0;
        }
        public void changeType(char T) {
            type = T;
            switch (type)
            {
                case 'P':
                    power = 2; attackRange = 2; trackRange = 8;
                    bonus = 10;
                    break;
                case 'B':
                    power = 2; attackRange = 3; trackRange = 0;
                    bonus = 10;
                    break;
                case 'R':
                    power = 2; attackRange = 1; trackRange = 8;
                    bonus = 1;
                    break;
                case 'D':
                    power = 0; attackRange = 0; trackRange = 0;
                    bonus = 0;
                    break;
            }
        }

        //根據方向在地圖上移動 也可以追擊玩家
        public void move(int[,,] map, Player A)
        {
            if (trackRange == 0 || this.isDead() || this.type == 'B')
                return;//如果不會追蹤(移動)或死掉
            
            Vector3D vect = track(A, trackRange);

            switch (vect)   //根據方向數決定走哪裡
            {
                case Vector3D.Xplus:
                    if (x + 1 < map.GetLength(0) && map[x + 1, y, z] == 0) //邊界條件 不寫會爆
                        x++; 
                    break;
                case Vector3D.Xsub:
                    if (x - 1 > 0 && map[x - 1, y, z] == 0)
                        x--;
                    break;
                case Vector3D.Yplus:
                    if (y + 1 < map.GetLength(0) && map[x, y + 1, z] == 0)
                        y++;
                    break;
                case Vector3D.Ysub:
                    if (y - 1 > 0 && map[x, y - 1, z] == 0)
                        y--;
                    break;
                case Vector3D.Zplus:
                    if (z + 1 < map.GetLength(0) && map[x, y, z + 1] == 0)
                        z++;
                    break;
                case Vector3D.Zsub:
                    if (z - 1 > 0 && map[x, y, z - 1] == 0)
                        z--;
                    break;
            }
            //location.moveForward(vect, 1);
           // if(location.inRange()))
        }
        //追蹤玩家 /*或逃跑*/
        public Vector3D track(Player A, int trackRange)
        {
           // int vect = rand.Next(-3,4);   //方向 [1:X方向 2:Y方向 3:Z方向]
            if (trackRange < 0) return Vector3D.Null;
            //在追蹤範圍內--還能改更好--
            if (Math.Abs(A.X - x) < Math.Abs(trackRange) &&
                Math.Abs(A.Y - y) < Math.Abs(trackRange) &&
                Math.Abs(A.Z - z) < Math.Abs(trackRange)   )
                    switch (rand.Next(0, 3))    //隨機選一個向量判斷
                    {
                        case 0:
                            if (A.X > x) return Vector3D.Xplus;
                            if (A.X < x) return Vector3D.Xsub;
                            break;
                        case 1:
                            if (A.Y > y) return Vector3D.Yplus;
                            if (A.Y < y) return Vector3D.Ysub;
                            break;
                        case 2:
                            if (A.Z > z) return Vector3D.Zplus;
                            if (A.Z < z) return Vector3D.Zsub;
                            break;
                    }
            
            return Vector3D.Null;
        }

        //攻擊玩家
        public bool attack(Player A)
        {
            if (!isDead())//這行應該也不用
                if (Math.Abs(A.X - x) <= attackRange && //在攻擊範圍內
                    Math.Abs(A.Y - y) <= attackRange && 
                    Math.Abs(A.Z - z) <= attackRange )
                {
                    A.addHP(-1 * power);
                    return true;
                }
            
            return false;   //無法攻擊
        }

        //是否在這個位置
        public bool isPoint(int X,int Y,int Z)
        {
            return (X == x && Y == y && Z == z);
        }

        //在plane平面上的座標 [A:左右向 B:上下向]
        public int getA(Plane plane)
        {
            if (plane == Plane.X) return y;
            else if (plane == Plane.Y) return z;
            else if (plane == Plane.Z) return x;
            else return 0;
        }
        public int getB(Plane plane)
        {
            if (plane == Plane.X) return z;
            else if (plane == Plane.Y) return x;
            else if (plane == Plane.Z) return y;
            else return 0;
        }

        //是否在(p = n)平面上 ex:("x" = 3)平面
        public bool onPlane(Plane p,int n)
        {
            switch (p)
            {
                case Plane.X:
                    return x == n;

                case Plane.Y:
                    return y == n;

                case Plane.Z:
                    return z == n;

                default:
                    return false;
            }
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
