using System.Drawing;

namespace My3DMaze
{
    class Map2D
    {
        private int[,] nmap;        //存牆壁的血量 2維地圖 the data of your palne

        public int map_size { get; private set; }       //地圖大小 正方形地圖 非圖片大小(64-256)

        // create a 2D-Map by size*size.
        public Map2D(int size)
        {
            this.map_size = size;
            this.nmap = new int[map_size, map_size];
        }

        // assign reference-Map to 2D-Map.
        public Map2D(int[,] refrenceMap)
        {
            this.map_size = refrenceMap.GetLength(0);
            this.nmap = refrenceMap;
        }

        // set grid(x,y).
        public void setGrid(int x, int y, int value)
        {
            this.nmap[x, y] = value;
        }

        // draw 2D-Map on graph.
        // range expand from center to showedSize
        // assume max of grid 
        public void drawOn(MapGraph graph , Point2D showedCenter , int showedSize, int maxnGridSize = 64)
        {
            int maxSize = showedSize * 2 + 1;
            graph.formatGrid(maxSize);
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
                    {
                        int alpha = nmap[mapX, mapY] * 255 / maxnGridSize;
                        if (alpha > 255) alpha = 255;
                        if (alpha < 0) alpha = 0;
                        color = Color.FromArgb(alpha, Color.Black);
                    }
                        

                    graph.drawGrid(i, j, color);
                }
            }
        }
    }
}
