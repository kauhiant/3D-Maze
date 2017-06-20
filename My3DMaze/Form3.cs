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
    public partial class Form3 : Form
    {
        private int mapSize = 32;
        private int R = 32;
        private int B = 128;
        private double monsterExp=0;//計算用
        private int MExp = 1;//怪物數量倍數

        public Form3()
        {
            InitializeComponent();
        }
        //自訂模式
        private void button4_Click(object sender, EventArgs e)
        {
            Form2 gameForm = new Form2();
            gameForm.Show();
        }
        //休閒模式
        private void button5_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(128, mapSize, 16, 0.45, MExp * B, MExp * R, 4,false,"休閒模式");
            gameForm.Show();
        }
        //拆牆模式
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(128, mapSize, 5, 0.5, MExp * B, MExp * R, 4,false,"拆牆模式");
            gameForm.Show();
        }
        //屠殺模式
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(32767, mapSize, 16, 0.5, MExp * B*2, MExp * R*8, 4,false,"屠殺模式");
            gameForm.Show();
        }
        //迷宮模式
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(128, mapSize, 128, 0.45, MExp * B, MExp * R, 4,false,"迷宮模式");
            gameForm.Show();
        }

        //調整地圖大小
        int size=1;
        private void button6_Click(object sender, EventArgs e)
        {
            if (size <= 4)
            {
                size*=2;
            }
            else
            {
                size = 1;
            }
            mapSize = size * 32;
            button6.Text = "地圖大小 : " + mapSize;

            //怪物密度固定
            //monsterExp = Log2(mapSize) - 5
            monsterExp = (Math.Log(mapSize) / Math.Log(2.0)) - 5.0;
            //MExp = 8 ^ monsterExp 
            MExp =Convert.ToInt32( Math.Pow(8.0,monsterExp) );
        }

        //遊戲說明
        private void button7_Click(object sender, EventArgs e)
        {
            Form4 gameForm = new Form4();
            gameForm.Show();
        }
        //教學模式
        private void button8_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(128, 32, 16, 0.45, 0, 0, 4,true,"教學模式");
            gameForm.Show();
        }
    }
}
