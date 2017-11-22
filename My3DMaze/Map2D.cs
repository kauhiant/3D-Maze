using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My3DMaze
{
    class Map2D
    {
        private int[,] nmap;        //存牆壁的血量 2維地圖 the data of your palne
        private int map_size;       //地圖大小 正方形地圖 非圖片大小(64-256)
        private Map3D refrenceMap;  //2D地圖是由3D地圖產生的

        

        public Map2D(Map3D refrenceMap)
        {
            this.refrenceMap = refrenceMap;
            this.map_size = refrenceMap.map_size;
            this.nmap = new int[map_size, map_size];
        }

        public Map2D(int[,] refrenceMap)
        {
           // this.refrenceMap = refrenceMap;
            this.map_size = refrenceMap.GetLength(0);
            this.nmap = refrenceMap;
        }

        //創造2維地圖
        public void creat2DMapOn(Plane plane,int value)
        {
            //--參考3維地圖建立2維地圖--
            for (int i = 0; i < map_size; i++)
                for (int j = 0; j < map_size; j++)
                    switch (plane)     //在哪個平面建立
                    {
                        case Plane.X:
                            nmap[i, j] = refrenceMap.map[value, i, j];
                            break;
                        case Plane.Y:
                            nmap[i, j] = refrenceMap.map[j, value, i];
                            break;
                        case Plane.Z:
                            nmap[i, j] = refrenceMap.map[i, j, value];
                            break;
                    }
        }

        public void drawOn(MapGraph graph , Point2D showedCenter , int showedSize)
        {
            int maxSize = showedSize * 2 + 1;
            graph.setGrid(maxSize);
            Color color;

            for (int i = 0; i < maxSize; ++i)
            {
                int mapX = showedCenter.x - showedSize + i;
                for(int j = 0; j < maxSize; ++j)
                {
                    int mapY = showedCenter.y - showedSize + j;
                    if (mapX >= nmap.GetLength(0) || mapY >= nmap.GetLength(1) || mapX < 0 || mapY < 0)
                        color = Color.Transparent;
                    else
                        color = Color.FromArgb(nmap[mapX, mapY] * 255 / refrenceMap.grid_hard, Color.Black);

                    graph.drawGrid(i, j, color);
                }
            }
        }
    }
}
