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
        Map3D mainMap;
        MapGraph test;
        Map2D map;
        Player me;
        Monster[] monsters;
        //picturebox 大小要是60的倍數
        int count = 0;
        public ForTest()
        {
            InitializeComponent();

            
            mainMap = new Map3D(64,20,0.5);
            map = new Map2D(mainMap);

            me = new Player(mainMap);

            
            map.creatMap(me.location, me.plane);

            test = new MapGraph(pictureBox1);
         //   pictureBox1.BackColor = Color.Blue;
            map.drawOn(test, me.location2d, Const.seenSize);
            me.showOn(test);
            test.update();

            monsters = new Monster[mainMap.map_size];
            for (int i = 0; i < mainMap.map_size; ++i)
                monsters[i] = new Monster(new Point3D(1,1,1),mainMap);
            
        }

        

        private void ForTest_KeyDown(object sender, KeyEventArgs e)
        {
            for (int i = 0; i < mainMap.map_size; ++i)
            {
                monsters[i].move(me);
                monsters[i].attack(me);
            }
                
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
                    me.location2d.changePlane(Plane.X);
                  //  test.backColorTo(Color.Red);
                    break;
                case Keys.Y:
                    me.location2d.changePlane(Plane.Y);
                  //  test.backColorTo(Color.Green);
                    break;
                case Keys.Z:
                    me.location2d.changePlane(Plane.Z);
                 //   test.backColorTo(Color.Blue);
                    break;
                case Keys.Space:
                    me.attack(mainMap);
                    break;

                case Keys.L:
                    me.addEnergy(10);
                    me.addHP(10);
                    me.addPower(5);
                    break;
            }
            map.creatMap(me.location, me.location2d.plane);
            map.drawOn(test, me.location2d, Const.seenSize);
            me.showOn(test);
            _information.Text = me.information();
            Text = me.location.ToString();
            test.update();
        }
    }
}
