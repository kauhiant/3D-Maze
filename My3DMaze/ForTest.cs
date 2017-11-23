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
        Point3D location3d;
        Map3D mainMap;
        Point2D location;
        MapGraph test;
        Map2D map;
        public ForTest()
        {
            InitializeComponent();

            location3d = new Point3D(32, 32, 32);
            location = new Point2D(location3d, Plane.Z);

            mainMap = new Map3D(64,20,0.5);
            map = new Map2D(mainMap);
         //   map.creat2DMapRefrence(location);
            map.creatMap(location3d, location.plane);

            test = new MapGraph(pictureBox1);
            pictureBox1.BackColor = Color.Blue;
            map.drawOn(test, location, 5);
            test.update();
            
        }

        

        private void ForTest_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    location.moveForward(Vector2D.Up);
                    break;
                case Keys.Down:
                    location.moveForward(Vector2D.Down);
                    break;
                case Keys.Left:
                    location.moveForward(Vector2D.Left);
                    break;
                case Keys.Right:
                    location.moveForward(Vector2D.Right);
                    break;

                case Keys.X:
                    location.changePlane(Plane.X);
                    test.backColorTo(Color.Red);
                    break;
                case Keys.Y:
                    location.changePlane(Plane.Y);
                    test.backColorTo(Color.Green);
                    break;
                case Keys.Z:
                    location.changePlane(Plane.Z);
                    test.backColorTo(Color.Blue);
                    break;
            }
            //  map.creat2DMapRefrence(location);
            map.creatMap(location3d, location.plane);
            map.drawOn(test, location, 5);
            Text = location3d.ToString();
            test.update();
        }
    }
}
