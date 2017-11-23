using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace My3DMaze
{
    class MapGraph
    {
        private System.Windows.Forms.PictureBox scene;
        private Graphics graph;
        private Bitmap _graph;
        private int divX;   //X軸分割成?個小格子
        private int divY;   //Y軸分割成?個小格子
        private int gridX;  //小格子的X大小
        private int gridY;  //小格子的Y大小

        public MapGraph(PictureBox scene)
        {
            this.scene = scene;
            this._graph = new Bitmap(scene.Width/4, scene.Height/4);
            this.graph = Graphics.FromImage(_graph);
        }

        public void setGrid(int divBase)
        {
            this.divX = divBase;
            this.divY = divBase;
            this.gridX = _graph.Width / divX;
            this.gridY = _graph.Height / divY;
        }

        public void setGrid(int divX,int divY)
        {
            this.divX = divX;
            this.divY = divY;
            this.gridX = scene.Width / divX;
            this.gridY = scene.Height / divY;
        }

        public void update()
        {
            this.scene.Image = _graph;
        }

        //Graph a grid on bitmap(small map)(mapGraph)
        //在Bitmap上畫一個格子(依照圖片大小和解析度決定要畫的範圍)
        public bool drawGrid(int x, int y, Color color)
        {
            if (x >= divX || y >= divY || x < 0 || y < 0) return false;

            int XEdge = (x + 1) * gridX;
            int YEdge = (y + 1) * gridY;

            for (int j = y * gridY; j < YEdge && j < scene.Height; j++)
                for (int i = x * gridX; i < XEdge && i < scene.Width; i++)
                {
                    //Graph a pixel on mapGraph
                    _graph.SetPixel(i, j, color);
                }

            return true;
        }

        public void clear()
        {
           // graph.Clear(Color.Transparent);
        }

        //Graph a grid on bitmap(small map)(mapGraph)
        //在Bitmap上畫一個圖片(圖片大小需要設定)
        public void draw2DMap( Image img, int x, int y)
        {
            if (x >= divX || y >= divY) return;
           
            //圖片縮放 img.resize(gridX,gridY)


            graph.DrawImage(img, x, y);
        }

        public void backColorTo(Color color)
        {
            scene.BackColor = color;
        }
    }
}
