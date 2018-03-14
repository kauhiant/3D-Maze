using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    public enum MonsterType { Red, Blue, Purple, Dead }

   /* abstract*/ class Monster
    {
        protected static Map3D  map;

        private short status=0;

        // [remove this constructor when remake is complete]
        protected char type = 'R';

        public Point3D location { get; protected set; } //位置
        public int X { get { return location.x; } }
        public int Y { get { return location.y; } }
        public int Z { get { return location.z; } }

        public int HP           { get; protected set; } //生命
        public int power        { get; protected set; } //力量
        public int attackRange  { get; protected set; } //攻擊範圍
        public int trackRange   { get; protected set; } //追蹤範圍
        public int bonus        { get; protected set; } //獎勵

        public Color color      { get; protected set; } //顏色
        public Image shape      { get; protected set; } //外觀
        

        public Monster(Point3D location, Map3D map)
        {
            Monster.map = map;
            this.location = location;
            map.beAttack(location, -1);
        }

        protected void initProperty(int HP,int power,int attackRange,int trackRange,int bonus = 1)
        {
            this.bonus = bonus;
            this.HP    = HP;
            this.power = power;
            this.attackRange = attackRange;
            this.trackRange  = trackRange;
        }

        protected void initShape(Color color,Image shape)
        {
            this.color = color;
            this.shape = shape;
        }
        

        public bool isDead()
        { return (HP == 0); }

        public void addAttackRange(int val)
        { attackRange += val; }
        public void addTrackRange(int val)
        { trackRange += val; }
        public void addPower(int val)
        { power += val; }
        public void addBonus(int val)
        { bonus += val; }
        public void addHP(int val) {
            HP += val;
            if (HP < 0) HP = 0;
        }

        //殺死怪物 並回傳獎勵
        public int KillMonster()
        {
            this.HP = 0;
            return bonus;
        }

        //根據方向在地圖上移動 也可以追擊玩家
        virtual public void move(Player A)
        {
            if (trackRange == 0)    return;

            Point3D target = this.location.copy();
            Vector3D vect = track(A, trackRange);

            if(vect == Vector3D.Null)
            {
                target.moveRandom();
            }
            else
            {
                target.moveForward(vect, 1);
            }

            if (map.valueAt(target) != 0 || !target.inRange(Monster.map.range))
            {
                return;
            }
            map.setValueAt(location, 0);
            map.setValueAt(target, -1);
            this.location.moveTo(target);
        }

        //追蹤玩家
        private Vector3D track(Player target, int trackRange)
        {
            //不在追蹤範圍內--還能改更好--
            if (target.location.distanceTo(this.location) > trackRange)
                return Vector3D.Null;

            switch (statusStep())    //選一個向量判斷
            {
                case 0:
                    if (target.X > X) return Vector3D.Xplus;
                    if (target.X < X) return Vector3D.Xsub;
                    break;
                case 1:
                    if (target.Y > Y) return Vector3D.Yplus;
                    if (target.Y < Y) return Vector3D.Ysub;
                    break;
                case 2:
                    if (target.Z > Z) return Vector3D.Zplus;
                    if (target.Z < Z) return Vector3D.Zsub;
                    break;
            }
            return Vector3D.Null;
        }

        // status = {0 -> 1 -> 2 -> 0}
        private int statusStep()
        {
            if (status == 255)
                status = 0;
            else
                status++;
            return status % 3;
        }

        //攻擊玩家
        virtual public bool attack(Player target)
        {
            if (power == 0) return false;
            if (isDead()) return false;   //無法攻擊
            if (target.location.distanceTo(this.location) > attackRange)
                return false;
               
            target.addHP(-1 * power);
            return true;
        }
        
        

        //是否在(p = n)平面上 ex:("x" = 3)平面
        public bool onPlane(Plane p, int planeFix=0)
        {
            return location.valueAtDimension(p.dimension) == p.value+planeFix;
        }

        public void showOn(MapGraph target, int x, int y)
        {
            target.drawGrid (x, y, this.color, (this.HP < 16) ? this.HP * 255 / 16 : 255);
            target.draw2DMap(x, y, this.shape);
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
        public int getA(Dimension plane)
        {
            return location.get2DPointOnPlane(plane).x;
        }
        public int getB(Dimension plane)
        {
            return location.get2DPointOnPlane(plane).y;
        }
    }
}
