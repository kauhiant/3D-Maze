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
    public partial class Form2 : Form
    {
        private int getHP = 64, getsSize = 32, getDens = 2, getWall = 16;
        private int getB = 8, getR = 128;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        double getWallDens = 0.5;
        public Form2()
        {
            InitializeComponent();
            

            textBox1.Text =getHP.ToString();
            textBox2.Text =getsSize.ToString();
            textBox3.Text =getWallDens.ToString();
            textBox4.Text =getWall.ToString();
            textBox5.Text =getDens.ToString();
            textBox6.Text =getR.ToString();
            textBox7.Text =getB.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                getHP = Convert.ToInt16(textBox1.Text);
                getsSize = Convert.ToInt16(textBox2.Text);
                getWallDens = Convert.ToDouble(textBox3.Text);
                getWall = Convert.ToInt16(textBox4.Text);
                getDens = Convert.ToInt16(textBox5.Text);
                getR = Convert.ToInt16(textBox6.Text);
                getB = Convert.ToInt16(textBox7.Text);

                getDens = Convert.ToInt16(Math.Pow(2.0, Convert.ToDouble(getDens)));
                Form1 gameForm = new Form1(getHP, getsSize, getWall, getWallDens, getB, getR, getDens);
                gameForm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("請按照格式輸入");
            }
        }
    }
}
