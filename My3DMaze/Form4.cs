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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label1.Text=
                "這是三維空間的地圖，二維平面的視角。"+Environment.NewLine+
                "顯示在視窗上的是一個二維平面，" + Environment.NewLine +
                "這是為了模擬我們處在二維平面，" + Environment.NewLine +
                "去思考三維空間，我們要移動，也只能在一個平面上，" + Environment.NewLine +
                "但我們可以藉由平面轉換，讓自己的視角轉換，移動方向也跟著改變。" + Environment.NewLine +
                "用三維空間的概念來講如下:" + Environment.NewLine +
                "我在Z平面我可以 前後左右移動" + Environment.NewLine +
                "我在Y平面我可以 上下左右移動" + Environment.NewLine +
                "我在X平面我可以 前後上下移動" + Environment.NewLine +
                "但是在二維平面上都是前後左右移動";
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            label1.Text =
                "用另一個說法來講:" + Environment.NewLine +
                "我前面有一道牆，我要到牆的對面，我可以" + Environment.NewLine +
                "方法一:" + Environment.NewLine +
                "STEP1: 往右走(方向: 左右)" + Environment.NewLine +
                "STEP2: 往前走(方向: 前後)" + Environment.NewLine +
                "STEP3: 往左走(方向: 左右)" + Environment.NewLine +
                "方法二:" + Environment.NewLine +
                "STEP1: 往上走(方向: 上下)" + Environment.NewLine +
                "STEP2: 往前走(方向: 前後)" + Environment.NewLine +
                "STEP3: 往下走(方向: 上下)" + Environment.NewLine +
                "現在觀測者在Z平面，他只有前後左右的概念" + Environment.NewLine +
                Environment.NewLine +
                "在方法一當中，觀測者可以看到你往右走、往前走、往左走，走到了牆壁對面" + Environment.NewLine +
                "在方法二當中，觀測者是看到你突然消失，又突然出現在牆壁的對面" + Environment.NewLine +
                "(在Z平面的觀測者只有前後左右的概念，並不知道還有上下)"; 

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            label1.Text =
                "移動	方向鍵	or  W A S D" + Environment.NewLine +
                "旋轉 X Z" + Environment.NewLine +
                "攻擊 空白鍵" + Environment.NewLine +
                "暫停 P" + Environment.NewLine +
                "返回 空白鍵" + Environment.NewLine +
                Environment.NewLine +
                "攻擊會消耗能量" + Environment.NewLine +
                "攻擊是全範圍的，但是距離只有1" + Environment.NewLine +
                "可以攻擊牆壁和怪物" + Environment.NewLine +
                "打死怪物有機會得到獎勵" + Environment.NewLine +
                "< HP + 1 | 力量 + 1 | 能量 + 1 >"+ Environment.NewLine+
                Environment.NewLine +
                "遊戲目標 : 走到地圖邊界";
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            label1.Text =
                "迷宮模式:燒腦袋" + Environment.NewLine +
                "牆壁密度低(0.45)" + Environment.NewLine +
                "牆壁厚度高(128)" + Environment.NewLine +
                "怪物密度低" + Environment.NewLine +
                "拆牆模式:一路拆到出口" + Environment.NewLine +
                "牆壁厚度低(5)" + Environment.NewLine +
                "怪物密度低" + Environment.NewLine +
                "屠殺模式:狂按空白鍵" + Environment.NewLine +
                "HP高(不要超過32768)" + Environment.NewLine +
                "怪物密度高" + Environment.NewLine +
                Environment.NewLine +
                "如果還想的到其他玩法麻煩通知我";

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            label1.Text =
                "HP          你的血量，等於0就死了" + Environment.NewLine +
                "地圖大小    地圖的邊長，假如設為10，那就是10x10x10 的地圖(三維的)" + Environment.NewLine +
                "牆壁密度    密度越高，越找不到路" + Environment.NewLine +
                "牆壁厚度    牆壁的血量，可以拆牆，牆壁血量越少越透明" + Environment.NewLine +
                "解析度     地圖的清晰度，數字越小清晰度越高，但是也很吃效能" + Environment.NewLine +
                "怪物紅     血量5攻擊2攻擊範圍1 會移動" + Environment.NewLine +
                "怪物藍     血量1攻擊2攻擊範圍3 不會移動"; 

        }

        private void 返回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
