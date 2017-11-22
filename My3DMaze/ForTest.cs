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
        MapGraph test;
        int[,] mainMap;
        Map2D map;
        int count = 0;
        public ForTest()
        {
            InitializeComponent();
            mainMap = new int[64,64];
            for(int i=0;i<64;++i)
                for(int j = 0; j < 64; ++j)
                {
                    if(i%3==j%3)
                    mainMap[i, j] = 200;
                }


            test = new MapGraph(pictureBox1);
            map = new Map2D(mainMap);
            pictureBox1.BackColor = Color.Green;
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            test.drawGrid(count, count, Color.Red);
            count++;
            test.update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Point2D tmp = new Point2D(0, count + 30);
            Text = tmp.ToString();
            button1.Text = (count + 30).ToString();
            map.drawOn(test, tmp, 5);
            test.update();
            ++count;
        }
    }
}
