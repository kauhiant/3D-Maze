using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My3DMaze
{
    public partial class ForTest : Form
    {
        //test draw2DMap
        Image testImg = Image.FromFile(@"./Shield.png");


        Map3D mainMap;
        MapGraph test;
        Map2D map;
        Player me;
        MonsterController originMonster;
        

        //picturebox 大小要是60的倍數
        int count = 0;


        public ForTest()
        {
            InitializeComponent();
              
            mainMap = new Map3D(64,20,0.5);
            me = new Player(mainMap,new Point3D(32,32,32),Dimension.Z,256);
            map = mainMap.creat2DMapOn(me.plane);
            Point3D originMonsterLocate = me.location.copy();
            originMonsterLocate.moveRandom(10);
            originMonster = new MonsterController(originMonsterLocate, mainMap);

            

            test = new MapGraph(pictureBox1,2);
            map.drawOn(test, me.location2d, Const.seenSize , mainMap.grid_hard);
            me.showOn(test);
            MonsterController.Initializer(me, test);
            test.update();
            
            monsterTimer.Interval = 500;
        }

        private void timeStart()
        {
            timer1.Enabled = true;
            monsterTimer.Enabled = true;
        }

        private void timePause()
        {
            timer1.Enabled = false;
            monsterTimer.Enabled = false;
        }

        private void ForTest_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    me.moveForward(Vector2D.Up);
                    break;
                case Keys.Down:
                    me.moveForward(Vector2D.Down);
                    break;
                case Keys.Left:
                    me.moveForward(Vector2D.Left);
                    break;
                case Keys.Right:
                    me.moveForward(Vector2D.Right);
                    break;

                case Keys.X:
                    me.location2d.changePlane(Dimension.X);
                      test.backColorTo(Color.FromArgb(100, Color.Red));
                    break;
                case Keys.Y:
                    me.location2d.changePlane(Dimension.Y);
                      test.backColorTo(Color.FromArgb(100, Color.Green));
                    break;
                case Keys.Z:
                    me.location2d.changePlane(Dimension.Z);
                       test.backColorTo(Color.FromArgb(100,Color.Blue));
                    break;
                case Keys.Space:
                    me.attack();
                    break;

                case Keys.L:
                    me.addEnergy(10);
                    me.addHP(10);
                    me.addPower(5);
                    break;

                case Keys.P:
                    timePause();
                    break;

                case Keys.O:
                    timeStart();
                    break;

                case Keys.M:
                    me.addSeenSize(1);
                    break;

                case Keys.N:
                    me.addSeenSize(-1);
                    break;
            }
            
            

        }

        private void ForTest_Load(object sender, EventArgs e)
        {
            timeStart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            me.addEnergy(1);
            mainMap.fix2DMapOn(map, me.plane);
            map.drawOn(test, me.location2d, me.seenSize, mainMap.grid_hard);
            me.showOn(test);
            MonsterController.show();
            test.update();
            _information.Text = me.information() + Environment.NewLine + originMonster.information();
            Text = me.location.ToString();

            if (me.HP == 0) {
                timePause();
                this.Text = "Lose";
            }

            if (me.location.onEdge(mainMap.range)) {
                timePause();
                this.Text = "Win";
            }
        }

        private void monsterTimer_Tick(object sender, EventArgs e)
        {
            MonsterController.action();
        }
    }
}
