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
        private Bitmap   buffer;
        private int divX;   //X軸分割成?個小格子
        private int divY;   //Y軸分割成?個小格子
        private double gridWidth;   //小格子的X大小
        private double gridHeight;  //小格子的Y大小

        // bind to outer scene 
        // density : [1:Clear, 2:Middle, 4:Blurry]
        // divBase : divide scene by given value
        public MapGraph(PictureBox scene,int density=1, int divBase = 16)
        {
            this.scene  = scene;
            this.buffer = new Bitmap(scene.Width/density, scene.Height/density);
            this.graph  = Graphics.FromImage(buffer);
            this.formatGrid(divBase);
            this.scene.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        // divide scene by given value.
        public void formatGrid(int divBase)
        {
            this.formatGrid(divBase, divBase);
        }

        // divide scene by given value.
        public void formatGrid(int divX,int divY)
        {
            this.divX = divX;
            this.divY = divY;
            this.gridWidth  = (double)buffer.Width  / divX;
            this.gridHeight = (double)buffer.Height / divY;
        }

        // update img of PictureBox.
        public void update()
        {
            this.scene.Image = buffer;
        }

        // draw color at grid(x,y).
        public void drawGrid(int x, int y, Color color)
        {
            if (x >= divX || y >= divY || x < 0 || y < 0) return;
            
            int XStart = Convert.ToInt32(x * gridWidth);
            int YStart = Convert.ToInt32(y * gridHeight);
            int XEdge  = Convert.ToInt32((x + 1) * gridWidth);
            int YEdge  = Convert.ToInt32((y + 1) * gridHeight);

            for (int j = YStart; j < YEdge; j++)
                for (int i = XStart; i < XEdge; i++)
                {
                    buffer.SetPixel(i, j, color);
                }
        }
        public void drawGrid(int x, int y, Color color, int alpha)
        {
            drawGrid(x, y, Color.FromArgb(alpha, color));
        }

        // draw a image at grid(x,y).
        public void draw2DMap(int x, int y, Image img, int alpha=255)
        {
            if (x >= divX || y >= divY) return;
            if (img == null) return;
            Bitmap origin = new Bitmap(img, (int)gridWidth, (int)gridHeight);
            Bitmap obj = getNewMapByAlpha(origin, alpha);

            graph.DrawImage(obj, (int)(gridWidth *x), (int)(gridHeight *y));
        }

        // set background of scene.
        public void backColorTo(Color color)
        {
            scene.BackColor = color;
        }

        // get a bitmap by new alpha
        private Bitmap getNewMapByAlpha(Bitmap origin, int alpha)
        {
            Bitmap obj = new Bitmap(origin.Width, origin.Height);
            for (int i = 0; i < obj.Height; ++i)
                for (int j = 0; j < obj.Width; ++j)
                {
                    obj.SetPixel(j, i, 
                        Color.FromArgb(origin.GetPixel(j, i).A * alpha / 255, 
                                       origin.GetPixel(j, i)    ));
                }
            return obj;
        }


        
    }
}
