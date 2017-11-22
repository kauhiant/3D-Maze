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
            mainMap = new Map3D(64,200,0.5);
            location3d = new Point3D(32,32,32);

            map = new Map2D(mainMap);
            location = new Point2D(32,32);

            location.bindWith(location3d, Plane.Z);

            test = new MapGraph(pictureBox1);
            pictureBox1.BackColor = Color.Green;
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
                    map.creat2DMapOn(Plane.X, location.planeValue);
                    break;
                case Keys.Y:
                    map.creat2DMapOn(Plane.Y, location.planeValue);
                    break;
                case Keys.Z:
                    map.creat2DMapOn(Plane.Z, location.planeValue);
                    break;
            }

            map.drawOn(test, location, 5);
            Text = location.ToString();
            test.update();
        }
    }
}
