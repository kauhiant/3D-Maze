using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class Monster
    {
        private Map3D map;
        protected Point3D location;//位置
        public int X { get { return location.x; } }
        public int Y { get { return location.y; } }
        public int Z { get { return location.z; } }

        public int HP           { get; protected set; } //生命
        public int power        { get; protected set; } //力量
        public int attackRange  { get; protected set; } //攻擊範圍
        public int bonus        { get; protected set; } //獎勵
        public Color color      { get; protected set; } //顏色

        char type;  //型態 { R:紅怪物 B:藍怪物 P:復仇者 }
        protected int trackRange; //追蹤範圍
        Random rand = new Random(); //亂數產生器

        //初始化 T:怪物型態 [X,Y,Z]:在地圖上的位置
        //第二代要做很多種怪物!!
        public Monster(Point3D location, Map3D map)
        {
            this.map = map;
            this.location = location;

            HP = 1; power = 0; attackRange = 1; trackRange = 1;
            color = Color.Red;
        }

        //殺死怪物 並回傳獎勵
        public int KillMonster()
        {
            HP = 0; power = 0; attackRange = 0; trackRange = 0;
            type = 'D';
            return bonus;
        }
        
        public bool isDead() { return (HP == 0); }
        
        public void addTrackRange(int r) { trackRange += r; }
        public void addPower(int n) { power += n; }
        public void addBonus(int n) { bonus += n; }
        public void addHP(int n) {
            HP += n;
            if (HP < 0) //如果HP低於零就歸零
                HP = 0;
        }
        

        //根據方向在地圖上移動 也可以追擊玩家
        public void move(Player A)
        {
            if (trackRange == 0 || this.isDead() || this.type == 'B')
                return;//如果不會追蹤(移動)或死掉
            
            Vector3D vect = track(A, trackRange);

            this.location.moveForward(vect, 1);
            if (!location.inRange(this.map.range))
                this.location.moveForward(vect, -1);
            
        }
        //追蹤玩家 /*或逃跑*/
        public Vector3D track(Player A, int trackRange)
        {
           // int vect = rand.Next(-3,4);   //方向 [1:X方向 2:Y方向 3:Z方向]
            if (trackRange < 0) return Vector3D.Null;
            //在追蹤範圍內--還能改更好--
            if (A.location.distanceTo(this.location) <= trackRange)
                    switch (rand.Next(0, 3))    //隨機選一個向量判斷
                    {
                        case 0:
                            if (A.X > X) return Vector3D.Xplus;
                            if (A.X < X) return Vector3D.Xsub;
                            break;
                        case 1:
                            if (A.Y > Y) return Vector3D.Yplus;
                            if (A.Y < Y) return Vector3D.Ysub;
                            break;
                        case 2:
                            if (A.Z > Z) return Vector3D.Zplus;
                            if (A.Z < Z) return Vector3D.Zsub;
                            break;
                    }
            
            return Vector3D.Null;
        }

        //攻擊玩家
        virtual public bool attack(Player A)
        {
            if (!isDead())//這行應該也不用
                if (A.location.distanceTo(this.location) <= attackRange)
                {
                    A.addHP(-1 * power);
                    Console.Write("attack");
                    return true;
                }
            
            return false;   //無法攻擊
        }

        //是否在這個位置
        public bool isPoint(int X,int Y,int Z)
        {
            return (X == this.X && Y == this.Y && Z == this.Z);
        }

        public bool inSeenRangeOfPlayer(Player player)
        {
            if (!onPlane(player.plane, player.getPlaneValue())) return false;

            Point2D tempLocation = new Point2D(this.location, player.plane);
            Range2D range = new Range2D(player.location2d, Const.seenSize);

            return tempLocation.inRange(range);
        }

        //是否在(p = n)平面上 ex:("x" = 3)平面
        public bool onPlane(Plane p,int n)
        {
            switch (p)
            {
                case Plane.X:
                    return X == n;

                case Plane.Y:
                    return Y == n;

                case Plane.Z:
                    return Z == n;

                default:
                    return false;
            }
        }

        public void showOn(MapGraph target, int x, int y)
        {
            target.drawGrid(x, y, this.color, (this.HP < 126) ? this.HP * 255 / 126 : 255);
        }

        //怪物成長
        public void growUp(Monster A)
        {
            addTrackRange(1);
            addBonus(A.getBonus());
            addPower(A.getPower());
            addHP(1);
        }

        

        //---------------------------------old----------------------------

        public Monster(char T, int X, int Y, int Z)
        {
            if (T == 'B' || T == 'b')
            {
                HP = 1;
                type = 'B';
            }
            else if (T == 'R' || T == 'r')
            {
                HP = 5;
                type = 'R';
            }
            else if (T == 'P' || T == 'p')
            {
                HP = 5;
                type = 'P';
            }
            else
            {
                HP = 0; type = 'D';
            }

            location = new Point3D(X, Y, Z);
            changeType(T);
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

        public char getType() { return type; }
        public int getHP() { return HP; }
        public int getPower() { return power; }
        public int getBonus() { return bonus; }

        //在plane平面上的座標 [A:左右向 B:上下向]
        public int getA(Plane plane)
        {
            if (plane == Plane.X) return Y;
            else if (plane == Plane.Y) return Z;
            else if (plane == Plane.Z) return X;
            else return 0;
        }
        public int getB(Plane plane)
        {
            if (plane == Plane.X) return Z;
            else if (plane == Plane.Y) return X;
            else if (plane == Plane.Z) return Y;
            else return 0;
        }
    }
}
