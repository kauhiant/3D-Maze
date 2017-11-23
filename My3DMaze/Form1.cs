using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 3D Maze I
//author : kauhiant
//Welcom to modify and share (^_^
//the map of this game is 3D ,but show that by 2D

namespace My3DMaze
{
    public partial class Form1 : Form
    {
        /* maps
         *  3D Map : the complete map of this game
         *  2D Map : the map to show on picturebox1
         *  Assist Map : the map of [(up or down) then your plane]
         *    Ex: if your plane is [x=15] then AssistMapUp is plane [x=14]
         *  grid-wall : the black grid of map , you can't pass that ,but you can break down that
         *    
         * draw bitmap
         *  grid and field : if set field 16 then will draw 16*16 grid on graph 
         *  density : if set 4 then graph 4*4 pixel each grid on bitmap
         *  
         * monsters
         *  red monster : it can move
         *  biue monster : it can't move ,but its attack range is broad
         *  purple monster : when a monster died , it will be stronger
         *  
         * */

        private CoolDownTime keyLock;   //  鍵盤鎖定，防止連續按太多下
        private Random rand;        //亂數產生器
        private bool isTeaching;    //是否是教學模式 is Teach Mode?
        private bool canWave;        //被攻擊時畫面是否會震動 when you be attacked, does the picturebox wave
        private bool canShowTwoMap; //右邊小地圖是否顯示 can assitMap show graph 

        //for Creat 3d map data
        private int map_size;       //地圖大小 正方形地圖 非圖片大小(64-256)
        private int grid_hard;      //牆壁厚度(血量) the HP of gridWall
        private double map_density; //牆壁密度(0.0-1.0) 

        //for Store Data of 3d and 2d map
        private int[,,] map;        //存牆壁的血量 3維地圖 the data of 3dMap
        private int[,] nmap;        //存牆壁的血量 2維地圖 the data of your palne

        //for Show Main 2d map on picturebox1
        private Bitmap mapGraph;    //要顯示在遊戲主畫面(picturebox1)的圖 the main picture in game
        private int graph_size;     //size of mapGraph
        private int field = 16;     //視野大小 (field格*field格) how many grid show on mapGraph (not pixel)
        private int grid_dens;      //解析度 (最高 1-2-4-8 最低) high to low [1 to 8]

        //Assist map
        private Bitmap assistMapUp;     //右邊小地圖 上 (_samllMapUp) the map showing on right
        private Bitmap assistMapDown;   //右邊小地圖 下 (_smallMapDown) the map showing on right
        private int[,] smapUp;          //右邊小地圖的資料 the data of assistMapUp
        private int[,] smapDown;        //同上the data of assistMapDown
        private int sField = 5;         //小地圖的field  The field of assist map 
        private int asMapGraph_size;    //showed size of assistMap

        //Data of player and monster
        private int initHP;                 //初始血量
        private Player you;                 //你的角色 your character
        private List<Monster> monsterList;  //怪物 List
        private int BMN;        //藍怪的數量 number of blue monsters
        private int RMN;        //紅怪的數量 number of red monsters
        
        //game Status
        private bool isWin;     //是否勝利
        private bool isLose;    //是否死亡
        private bool isLock;    //是否暫停中 is pausing?
        
        //-----------------------------------------------------------------------------------------------

        //遊戲初始化 game inition
        public Form1(int _HP=64,int _size=32,int _wallHard=5,double _wallDens=0.6,
            int _BN=10,int _RN=100,int _graphDens=4,bool isTeachMode=false,string TITLE="自訂模式")
        {
            InitializeComponent();
            this.Text = TITLE;  

            //上排輔助功能設定
            canWave = true;          //被攻擊會震動 when you be attack ,the picturebox wave
            canShowTwoMap = true;   //右側小地圖會顯示 show assist map on right

            //設定顯示在地圖上的文字
            //文字背景透明
            //for show text on picturebox
            _hp.Parent = pictureBox1;
            _status.Parent = pictureBox1;
            _position3d.Parent = pictureBox1;

            _hp.BackColor = Color.Transparent;
            _status.BackColor = Color.Transparent;
            _position3d.BackColor = Color.Transparent;
            
            _hp.Left = 240;
            _position3d.Left = 190;
            _status.Left = 10;
            _status.Top = 10;

            _hp.ForeColor = Color.Snow;
            _status.ForeColor = Color.Snow;
            _position3d.ForeColor = Color.Snow;
            //鍵盤鎖定
            keyLock = new CoolDownTime(100);
            //亂數產生器
            rand = new Random();

            //遊戲狀態初始化
            isWin = false;
            isLose = false;
            isLock = false;
            
            //把引入參數的值存進來
            //import arguments to this form
            grid_hard = _wallHard;
            map_size = _size;
            map_density = _wallDens;
            initHP = _HP;
            BMN = _BN;
            RMN = _RN;

            //Const 設定
            Range1D tmpRange = new Range1D(map_size);
            Const.mazeRange = new Range3D(tmpRange , tmpRange , tmpRange);

            //for modify resolution
            grid_dens = _graphDens;
            graph_size = pictureBox1.Size.Height / grid_dens;   //圖片大小依解析度調整

            //is teach mode?
            isTeaching = isTeachMode;   //是否為教學模式

            //Create data of 3D Map and Initiate it
            //--創造3維地圖-- 
            map = new int[map_size, map_size, map_size];
            //初始化3維地圖的資料
            for (int i = 0; i < map_size; i++)
                for (int j = 0; j < map_size; j++)
                    for (int k = 0; k < map_size; k++)
                        if (rand.NextDouble() > map_density)    //依照牆壁密度建立迷宮
                            map[i, j, k] = 0;   //路 you can pass
                        else
                            map[i, j, k] = grid_hard;   //牆壁血量初始化 you can't pass
            
            
            //--創造你的角色 放置於地圖中央 Z平面 初始化血量--
            //create you and you are in center of the map
            you = new Player(map_size / 2, map_size / 2, map_size / 2, Plane.Z, initHP);
            

            //Create each monster by BMN RMN and place in random position
            //--創造怪物--
            monsterList = new List<Monster>();
            //產生三個亂數 用來設定復仇者的初始位置 the avenger
            int tmp1 = rand.Next(0, map_size);
            int tmp2 = rand.Next(0, map_size);
            int tmp3 = rand.Next(0, map_size);
            monsterList.Add(new Monster('P', tmp1, tmp2, tmp3));//'P' is 復仇者
            //產生紅怪 red monsters
            for (int i = 0; i < RMN; i++)
            {
                tmp1 = rand.Next(0, map_size);
                tmp2 = rand.Next(0, map_size);
                tmp3 = rand.Next(0, map_size);
                if (tmp1 != tmp2 && tmp2 != tmp3)
                    monsterList.Add(new Monster('R', tmp1, tmp2, tmp3));
                else
                    i--;
            }
            //產生藍怪 blue monsters
            for (int i = 0; i < BMN; i++)
            {
                tmp1 = rand.Next(0, map_size);
                tmp2 = rand.Next(0, map_size);
                tmp3 = rand.Next(0, map_size);
                if (tmp1 != tmp2 && tmp2 != tmp3)
                    monsterList.Add(new Monster('B', tmp1, tmp2, tmp3));
                else
                    i--;
            }
            
            
            //Creat bitmap of 2D Map
            //創造2維地圖的 Bitmap
            mapGraph = new Bitmap(graph_size/8, graph_size/8);

            //Creat storage of 2D map
            //創造2維地圖的資料空間
            nmap = new int[map_size, map_size];
            

            //Creat storage of two assist map 創造小地圖的資料空間
            smapUp = new int[sField, sField];
            smapDown = new int[sField, sField];

            //Create Bitmap of assistMap 創造輔助小地圖的 Bitmap
            asMapGraph_size =_smallMapUp.Size.Height/grid_dens;
            assistMapUp=new Bitmap(asMapGraph_size,asMapGraph_size);
            assistMapDown=new Bitmap(asMapGraph_size,asMapGraph_size);

            creat2DMap();   //創造主地圖的資料

            //Draw and Show 2d map on picturebox1
            //畫出主地圖並輸出
            drawMapGraph();

            //顯示角色狀態
            //show your infomation (text)
            showInfo();

            //Vector[X,Y,Z] = Color[Red,Green,Blue]
            //向量視覺化 show two vector on [toRight , toBotom] of main picture
            showIJK(Plane.Z);
            //平面視覺化 show vector on your plane [toInner]
            pictureBox1.BackColor = Color.FromArgb(100, Color.Blue);
            

            //遊戲教學 teach mode
            if (isTeaching)
            {
                isLock = true;
                _status.Text = "請按空白鍵開始";
            }
        }
        
        //--------------------------------------------------------------------------------------
                
        //Show your infomation on right Form
        //顯示資訊
        private void showInfo()
        {
            //right 右邊資訊表
            _monsters.Text="剩餘怪物 : " + monsterList.Count;
            _score.Text =  "分數 : " + you.score;
            _energy.Text = "能量 : " + you.energy;
            _power.Text =  "力量 : " + you.power;
            _plane.Text = you.getPlaneString();

            //center 主畫面 main picture
            _hp.Text = "HP : "+you.HP;
            _position3d.Text = you.showPoint();

            //輔助小地圖的平面
            //show the plane of assist map
            if (canShowTwoMap)
            {
    //            _smallPlaneUp.Text = you.getPlane() + " = " + (you.get(you.getPlane()) - 1);
     //           _smallPlaneDown.Text = you.getPlane() + " = " + (you.get(you.getPlane()) + 1);
            }
            else
            {
                _smallPlaneUp.Text = "";
                _smallPlaneDown.Text = "";
            }
        }

        //-----------------------------------------------------------------------------------------------

        //讓玩家更直覺分辨IJK向量 [右方 下方 by 顏色]
        //[紅:X方向 綠:Y方向 藍:Z方向]
        //Vector[X,Y,Z] = Color[Red,Green,Blue]
        //向量視覺化 show two vector on [toRight , toBotom] of main picture
        //refer [plane V] to show colorVector
        private void showIJK(Plane plane)  //V is a plane
        {
            switch (plane)  //在哪個平面 標示顏色
            {
                case Plane.X:
                    _colorDown.BackColor = Color.FromArgb(0,0,255);
                    _colorRight.BackColor = Color.FromArgb(0,255,0);
                    pictureBox1.BackColor = Color.FromArgb(100, Color.Red);
                    break;
                case Plane.Y:
                    _colorDown.BackColor = Color.FromArgb(255,0,0);
                    _colorRight.BackColor = Color.FromArgb(0,0,255);
                    pictureBox1.BackColor = Color.FromArgb(100, Color.Green);
                    break;
                case Plane.Z:
                    _colorDown.BackColor = Color.FromArgb(0,255,0);
                    _colorRight.BackColor = Color.FromArgb(255,0,0);
                    pictureBox1.BackColor = Color.FromArgb(100, Color.Blue);
                    break;
            }
        }
        
        //-------------------------------------------------------------------------------------------------

        //for Show 2d map on pictureBox
        //this is data
        //nmap : Big 2D Map
        //* when you transer plane
        //* fix the 2d map(nmap)
        //創造2維地圖
        private void creat2DMap()
        {
            showIJK(you.plane);    //顯示輔助顏色

            //--參考3維地圖建立2維地圖--
            for (int i = 0; i < map_size; i++)
                for (int j = 0; j < map_size; j++)
                    switch (you.plane)     //在哪個平面建立
                    {
                        case Plane.X:
                            nmap[i, j] = map[you.location.x, i, j];
                            break;
                        case Plane.Y:
                            nmap[i, j] = map[j, you.location.y, i];
                            break;
                        case Plane.Z:
                            nmap[i, j] = map[i, j, you.location.z];
                            break;
                    }

            //創造輔助地圖資料
            if(canShowTwoMap)   creatAssitMap();
                
            //Draw this data on Bitmap(mapGraph)
            //bitmap : small 2d map
            //畫主地圖
            drawMapGraph();
        }

        //for two assist map sField*sField grid
        //--參考3維地圖 建立輔助地圖的資料--
        private void creatAssitMap()
        {
            int sFix = sField / 2;  //小地圖的中心
            //根據 sField 大小建立資料
            for (int i = -1 * sFix; i < (sField - sFix); i++)
                for (int j = -1 * sFix; j < (sField - sFix); j++)
                    //沒有超出邊界
                    if (you.location.inRange(Const.mazeRange) || you.location.onEdge(Const.mazeRange))
                        //找出你的[視角]在哪個平面並建立[之上]與[之下]的資料
                        switch (you.plane)
                        {
                            case Plane.X:
                                smapUp[i + sFix, j + sFix] = map[you.X - 1, you.getA() + i, you.getB() + j];
                                smapDown[i + sFix, j + sFix] = map[you.X + 1, you.getA() + i, you.getB() + j];
                                break;
                            case Plane.Y:

                                smapUp[i + sFix, j + sFix] = map[you.getB() + j, you.Y - 1, you.getA() + i];
                                smapDown[i + sFix, j + sFix] = map[you.getB() + j, you.Y + 1, you.getA() + i];
                                break;
                            case Plane.Z:
                                smapUp[i + sFix, j + sFix] = map[you.getA() + i, you.getB() + j, you.Z - 1];
                                smapDown[i + sFix, j + sFix] = map[you.getA() + i, you.getB() + j, you.Z + 1];
                                break;
                        }
                    //超出邊界 都設定為 0
                    else
                    {
                        smapUp[i + sFix, j + sFix] = 0;
                        smapDown[i + sFix, j + sFix] = 0;
                    }
        }

        //--draw assit map
        //畫出輔助地圖
        private void drawAssitMap()
        {   
            //--draw grid wall-- 畫出牆壁
            for (int j = 0; j < sField; j++)
                for (int i = 0; i < sField; i++)
                {   //透明度 grid血越薄越透明
                    draw2DMap(i, j, Color.Black, smapUp[i, j] * 255 / grid_hard, ref assistMapUp, asMapGraph_size, sField);
                    draw2DMap(i, j, Color.Black, smapDown[i, j] * 255 / grid_hard, ref assistMapDown, asMapGraph_size, sField);
                }
            
            //--draw your position-- 畫出你的對照位置
            draw2DMap(sField / 2, sField / 2, Color.Green, 200, ref assistMapUp, asMapGraph_size, sField);
            draw2DMap(sField / 2, sField / 2, Color.Green, 200, ref assistMapDown, asMapGraph_size, sField);
            
            //--draw each monster-- 畫出怪物
            drawEachMonster(sField, 255, you.plane, ref assistMapUp, -1);
            drawEachMonster(sField, 255, you.plane, ref assistMapDown, 1);
        }

        //Draw nmap data on Bitmap
        //mapGraph : small 2d map (bitmap)
        //draw graph on bitmap and layout on pictureBox
        //1. Draw grid-wall
        //2. Draw character
        //3. Draw monsters
        //4. Layout on picturebox
        //畫出主地圖並輸出
        public void drawMapGraph()
        {
            int aph = 255 / grid_hard; ;   //透明度 grid血越薄越透明
            int toFix = field / 2;  //center of small map 

            //--Draw grid-wall--畫出牆壁
            drawGridWall();
            
            //--Draw your character--畫出你的角色
            aph =150 + you.HP * 8;
            if (aph > 255) aph = 255;
            if (aph == 150) aph = 0;
            draw2DMap(toFix, toFix, Color.Green, aph,ref mapGraph, graph_size, field);
            
            //--Draw monsters--畫出怪物
            drawEachMonster(field,aph,you.plane,ref mapGraph);

            //--Layout on pictureBox-- 
            //輸出          
            pictureBox1.Image = mapGraph;

            //--Draw two small map--
            //畫出輔助地圖並輸出
            if (canShowTwoMap)
            {
                drawAssitMap();
                _smallMapUp.Image = assistMapUp;
                _smallMapDown.Image = assistMapDown;
            }
           
        }

        //畫牆壁
        private void drawGridWall()
        {
            int toFix = field / 2;  //center of small map 
            int a = 0, b = 0;       //small map (x,y)
            
            for (int j = you.getB() - toFix; j < you.getB() + toFix; j++)
            {
                a = 0;
                for (int i = you.getA() - toFix; i < you.getA() + toFix; i++)
                {
                    //是否超出邊界
                    if (i < nmap.GetLength(0) && j < nmap.GetLength(1)
                        && i >= 0 && j >= 0)    
                        //牆壁越薄 越透明 .
                        draw2DMap(a, b, Color.Black, (nmap[i, j] * 255 / grid_hard), ref mapGraph, graph_size, field);
                    else
                        draw2DMap(a, b, Color.Black, 0, ref mapGraph, graph_size, field);
                    a++;
                }
                b++;
            }
        }

        
        //畫出單一怪物
        public void drawMonster(int index,int aph,ref Bitmap graph,int a,int b,int field)
        {
            int GSize = graph.Size.Height;
            aph = 30 * monsterList[index].getHP() + 100;
            if (aph > 255) aph = 255;
            //type of monster (Blue or Red)
            if (monsterList[index].getType() == 'B')
                draw2DMap(a, b, Color.Blue, 255, ref graph, GSize, field);
            else if (monsterList[index].getType() == 'R')
                draw2DMap(a, b, Color.Red, aph, ref graph, GSize, field);
            else
                draw2DMap(a, b, Color.Purple, aph, ref graph, GSize, field);
        }

        //畫出每隻怪物
        public void drawEachMonster(int size,int aph,Plane thisPlane,ref Bitmap mapGraph, int planeFix = 0)
        {
            //-for each monster-
            int a = 0, b = 0;
            int toFix = size / 2;
            for (int index = 0; index < monsterList.Count; index++)
            {
                //是否跟玩家在同一平面 或修正後的平面
                if (monsterList[index].onPlane(you.plane, you.getPlaneValue()+planeFix))
                {
                    b = 0;
                    int startA = you.getA() - toFix;
                    int startB = you.getB() - toFix;
                    int endA;
                    int endB;
                    if (size%2 == 0)
                    {
                        endA = you.getA() + toFix;
                        endB = you.getB() + toFix;
                    }
                    else
                    {
                        endA = you.getA() + toFix+1;
                        endB = you.getB() + toFix+1;
                    }

                    for (int j = startB; j < endB; j++)
                    {
                        a = 0;
                        for (int i = startA; i < endA; i++)
                        {
                            //is monster on this point(x:A,y:B)?
                            if (monsterList[index].getA(thisPlane) == i &&
                                monsterList[index].getB(thisPlane) == j)
                            {
                                drawMonster(index, aph, ref mapGraph, a, b,size);
                            }
                            a++;
                        }
                        b++;
                    }
                }
            }
        }


        //Graph a grid on bitmap(small map)(mapGraph)
        //在Bitmap上畫一個格子(依照圖片大小和解析度決定要畫的範圍)
        public void draw2DMap(int a,int b,Color c,int aph,ref Bitmap Obj,int Gsize,int gridBase)
        {
            int gBase = Gsize / gridBase;
            //  \ 十字定位法 /
            for (int j = b*gBase; j < (b+1)*gBase; j++)
                for (int i = a*gBase; i < (a+1)*gBase; i++)
                {
                    //Graph a pixel on mapGraph
                    Obj.SetPixel(i, j, Color.FromArgb(aph, c));
                }
        }
        
        //Attack
        //search each monster
        //if it is near by you
        //attack it
        //攻擊怪物
        //搜尋每一個怪物
        //如果在自己的攻擊範圍內
        //就攻擊他
        public void attack()
        {
            for(int i = 0; i < monsterList.Count; i++)
            {
                int x = monsterList[i].getX();
                int y = monsterList[i].getY();
                int z = monsterList[i].getZ();
                
                //is near by you ?
                if (x - 1 <= you.X && you.X <= x + 1 &&
                y - 1 <= you.Y && you.Y <= y + 1 &&
                z - 1 <= you.Z && you.Z <= z + 1)
                {
                    monsterList[i].addHP(-1*you.power);
                    
                    //is monster dead
                    if (monsterList[i].isDead())
                    {
                        //Purple monster grow up
                        //kill monster 
                        //remove it in list
                        //get bonus
                        monsterList[0].growUp(monsterList[i]);
                        int tmp = monsterList[i].KillMonster();
                        monsterList.RemoveAt(i);
                        if (i == 0) monsterList[0].changeType('P');     //殺了一個復仇者還有千千萬萬個復仇者
                        you.addScore(tmp);
                        while (tmp-- > 0)
                            you.getBonus(rand.NextDouble());
                    }
                }
            }
        }
        //monster attack you and move
        //怪物攻擊你
        //搜尋所有怪物 並讓他們移動
        //攻擊你
        public void monsterAttack()
        {
            for (int i = 0; i < monsterList.Count; i++)
            {
                monsterList[i].move(map,you);

                if (monsterList[i].attack(you))
                    graphWave();
                if (you.HP == 0)
                {
                    isLose = true;
                    _status.Text = "YOU LOSE";
                    _status.Font = new Font("微軟正黑體", 60);
                    timer1.Enabled = false;
                }
            }
        }
        
        

        //play game with 鍵盤
        //鍵盤控制
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyLock.isCoolDown()) return;

            keyLock.record();

            if (!(isWin || isLose) && !isLock)
            {

                switch (e.KeyCode)
                {
                    case Keys.W:
                    case Keys.Up:
                        isWin = you.move(Vector2D.Up,ref map,ref nmap,map_size);
                        if (indexTeach == 0) indexTeach++;
                        creat2DMap();
                        break;
                    case Keys.S:
                    case Keys.Down:
                        isWin = you.move(Vector2D.Down,ref map, ref nmap,map_size);
                        if (indexTeach == 0) indexTeach++;
                        creat2DMap();
                        break;
                    case Keys.A:
                    case Keys.Left:
                        isWin = you.move(Vector2D.Left,ref map,ref nmap, map_size);
                        if (indexTeach == 0) indexTeach++;
                        creat2DMap();
                        break;
                    case Keys.D:
                    case Keys.Right:
                        isWin = you.move(Vector2D.Right,ref map, ref nmap,map_size);
                        if (indexTeach == 0) indexTeach++;
                        creat2DMap();
                        break;
                    case Keys.X:
                        you.turn("right",ref map);
                        if (indexTeach == 1) indexTeach++;
                        creat2DMap();
                        break;
                   
                    case Keys.Z:
                        you.turn("down",ref map);
                        if (indexTeach == 1) indexTeach++;
                        creat2DMap();
                        break;
                    case Keys.L:
                        you.EnergyConvertHP();
                        break;
                    case Keys.Space:
                        you.attack(ref map);
                        attack();
                        creat2DMap();
                        if (indexTeach == 2) indexTeach++;
                        break;
                    case Keys.P:
                        _status.Text = "按空白鍵繼續";
                        timer1.Enabled = false;
                        isLock = true;
                        break;
                }
                //show your property and your location
                showInfo();
            }

            

            //從暫停中返回遊戲
            if (e.KeyCode == Keys.Space && isLock)
            {
                timer1.Enabled = true;
                isLock = false;
                _status.Text = _status.Text = "";
                if (isTeaching && indexTeach==0)
                    teachMove();
            }
            



            //teach mode

            if (isTeaching)
                switch (indexTeach)
                {
                    case 0:
                        teachMove();
                        break;
                    case 1:
                        teachTransfer();
                        break;
                    case 2:
                        teachAttack();
                        break;
                    case 3:
                        teachRed();
                        break;
                    case 4:
                        teachBlue();
                        break;
                    case 5:
                        teachFinal();
                        break;
                }
            

            //走到邊界了
            if (isWin)
            {
                _status.Text = "YOU WIN";
                _status.Font = new Font("微軟正黑體",60);
                timer1.Enabled = false;
            }
            //exit
            if (e.KeyCode == Keys.Space && (isWin || isLose))
            {
                this.Close();
            }


        }
        private int indexTeach = 0;
        //Game Time
        private void timer1_Tick(object sender, EventArgs e)
        {
            //_status.Text = "";

            you.addEnergy(1);
            monsterAttack();
            
            creat2DMap();
            showInfo();
        }

        //Wave one time
        private bool isWave = false;
        private void graphWave()
        {
            if (canWave)
                if (!isWave)
                {
                    isWave = true;
                    pictureBox1.Left += 6;
                    _colorRight.Left += 6;
                    _wave.Enabled = true;
                }
        }
        private void _wave_Tick(object sender, EventArgs e)
        {
            pictureBox1.Left -= 6;
            _colorRight.Left -= 6;
            _wave.Enabled = false;
            isWave = false;
        }

        //效果控制表
        
        private void 攻擊震動ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canWave = !canWave;
            if (canWave)
            {
                攻擊震動ToolStripMenuItem.Text = "攻擊震動 (開)";
                攻擊震動ToolStripMenuItem.BackColor = Color.Snow;
            }
            else
            {
                攻擊震動ToolStripMenuItem.Text = "攻擊震動 (關)";
                攻擊震動ToolStripMenuItem.BackColor = Color.Gray;
            }
                

        }
        
        private void 上下地圖ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canShowTwoMap = !canShowTwoMap;

            if (canShowTwoMap)
            {
                上下地圖ToolStripMenuItem.Text = "上下地圖 (開)";
                上下地圖ToolStripMenuItem.BackColor = Color.Snow;
                _smallMapUp.Visible = true;
                _smallMapDown.Visible = true;
            }
            else
            {
                上下地圖ToolStripMenuItem.Text = "上下地圖 (關)";
                上下地圖ToolStripMenuItem.BackColor = Color.Gray;
                _smallMapUp.Visible = false;
                _smallMapDown.Visible = false;
            }
        }
        private bool canShowTransfer = true;
        private void 轉換效果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canShowTransfer = !canShowTransfer;
            if (canShowTransfer)
            {
                //轉換效果ToolStripMenuItem.Text = "轉換效果 (開)";
                //轉換效果ToolStripMenuItem.BackColor = Color.Snow;
            }
            else
            {
                //轉換效果ToolStripMenuItem.Text = "轉換效果 (關)";
                //轉換效果ToolStripMenuItem.BackColor = Color.Gray;
            }
                
            MessageBox.Show("目前沒靈感 做不出來");
        }


        //--Teaching Mode--
        private bool isMoved = false;
        public bool teachMove()
        {
            _status.Text = "<移動>"+Environment.NewLine+
                "用方向鍵控制上下左右移動"+Environment.NewLine
                +"試著移動一步";
            if (isMoved) return true;
            else return false;
        }
        private bool isTransfer = false;
        public bool teachTransfer()
        {
            _status.Text = "<轉移>"+Environment.NewLine+
                "如果周圍都被擋住了無法移動"+Environment.NewLine+
                "那就使用這個遊戲的特色 [轉移]"+ Environment.NewLine+
                "按下 <X 或 Z> 就能往左或往下旋轉"+ Environment.NewLine+
                "!!請特別注意邊框顏色和地圖顏色!!"+ Environment.NewLine+
                "每個顏色代表不同方向"+ Environment.NewLine+
                "< X是紅色 Y是綠色 Z是藍色 >"+ Environment.NewLine+
                "在轉移時 一定要記好方向和座標"+ Environment.NewLine+
                "這樣才不會迷路喔"+ Environment.NewLine+
                "現在 試著轉移看看吧" + Environment.NewLine +
                "<按 Z 或 X 轉移>";
            return isTransfer;
        }
        private bool isAttacked = false;
        public bool teachAttack()
        {
            _status.Text = "<攻擊>" + Environment.NewLine +
                "如果轉移後也沒路可走"+Environment.NewLine+
                "那就把牆壁破壞掉"+ Environment.NewLine+
                "現在試著攻擊看看"+ Environment.NewLine+
                "<按空白鍵攻擊>";
            return isAttacked;
        }
        private bool isKillRed = false;
        public bool teachRed()
        {
            _status.Text = "<紅怪物>" + Environment.NewLine +
                "空白鍵不只可以拆牆還可以打怪" + Environment.NewLine +
                "現在就來介紹紅怪物" + Environment.NewLine +
                "< 生命5 力量2 攻擊範圍1 >" + Environment.NewLine +
                "殺死後可獲的一分" + Environment.NewLine +
                "也有機會獲的額外獎勵"+ Environment.NewLine+
                "現在就去消滅紅怪物吧" + Environment.NewLine ;
            
            if (you.score != 0)
            {
                isKillRed = true;
                indexTeach++;
            }
            else if (!isKillRed && monsterList.Count == 1)
            {
                monsterList.Add(new Monster('R', you.X, you.Y, you.Z + 1));
                creat2DMap();
                showInfo();
                timer1.Enabled = false;
                isLock = true;
            }
            if (isLock) _status.Text += "<按空白鍵繼續>";
            return isKillRed;
        }
        private bool isKillBlue = false;
        public bool teachBlue()
        {
            _status.Text = "<藍怪物>" + Environment.NewLine +
                "< 生命1 力量2 攻擊範圍3 >" + Environment.NewLine +
                "藍怪物最難纏的地方就是攻擊範圍" + Environment.NewLine +
                "他在你攻擊不到的地方攻擊你" + Environment.NewLine +
                "也很難找到他" + Environment.NewLine +
                "但他也有一個弱點 就是不會移動" + Environment.NewLine +
                "現在就去把它找出來並消滅掉" + Environment.NewLine +
                "殺死他的獎勵可是紅怪物的十倍呢" + Environment.NewLine;
            
            if (you.score != 1)
            {
                isKillBlue = true;
                indexTeach++;
            }
            else if (!isKillBlue && monsterList.Count == 1)
            {
                monsterList.Add(new Monster('B', you.X+2, you.Y+2, you.Z + 2));
                creat2DMap();
                showInfo();
                timer1.Enabled = false;
                isLock = true;
            }
            if (isLock) _status.Text += "<按空白鍵繼續>";
            return isKillBlue;
        }
        public void teachFinal()
        {
            _status.Text = "現在 你已經了解操作方法了" + Environment.NewLine +
                "我來介紹一下右邊的各種資訊" + Environment.NewLine +
                "能量 每次攻擊都會消耗一點 也會隨時間恢復" + Environment.NewLine +
                "力量 就是攻擊力 沒什麼好講的"+ Environment.NewLine+
                "剩餘怪物 現在剩餘的怪物數量"+ Environment.NewLine+ 
                "還有一點很重要 當你快死的時候 按[L]"+Environment.NewLine+
                "以上都了解了嗎"+Environment.NewLine+
                "現在就應用這些知識走到迷宮的邊界吧";
        }
    }
    
}

/* 
    private void showInfo()
    private void showIJK(string V)

    private void creat2DMap()
    private void creatAssitMap()

    private void drawAssitMap()
    public void drawMapGraph()
    private void drawGridWall()
    public void drawMonster(int index,int aph,ref Bitmap graph,int a,int b,int field)
    public void drawEachMonster(int size,int aph,string thisPlane,ref Bitmap mapGraph, int planeFix = 0)
    public void draw2DMap(int a,int b,Color c,int aph,ref Bitmap Obj,int Gsize,int gridBase)

    public void attack()
    public void monsterAttack()

    private void Form1_KeyDown(object sender, KeyEventArgs e)

    private void timer1_Tick(object sender, EventArgs e)
    private void graphWave()
    private void _wave_Tick(object sender, EventArgs e)

    private void 攻擊震動ToolStripMenuItem_Click(object sender, EventArgs e)
    private void 上下地圖ToolStripMenuItem_Click(object sender, EventArgs e)
    private void 轉換效果ToolStripMenuItem_Click(object sender, EventArgs e)

    public bool teachMove()
    public bool teachTransfer()
    public bool teachAttack()
    public bool teachRed()
    public bool teachBlue()
    public void teachFinal()

    private void _yourControl_Tick(object sender, EventArgs e)
*/
