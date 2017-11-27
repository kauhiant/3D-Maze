using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    class Map3D
    {
        private Random rand = new Random();

        public int map_size { get; private set; }        //地圖大小 正方形地圖 非圖片大小(64-256)
        public int grid_hard { get; private set; }       //牆壁厚度(血量) the HP of gridWall
        public double map_density { get; private set; }  //牆壁密度(0.0-1.0) 
        public Range3D range { get; private set; }

        //for Store Data of 3d and 2d map
        public int[,,] map { get; private set; }         //存牆壁的血量 3維地圖 the data of 3dMap

        public Map3D(int mapSize,int wallHard, double desity)
        {
            map_size = mapSize;
            grid_hard = wallHard;
            map_density = desity;
            range = new Range3D(new Range1D(map_size), new Range1D(map_size), new Range1D(map_size));

            //Create data of 3D Map and Initiate it
            //--創造3維地圖-- 
            map = new int[map_size, map_size, map_size];
            //初始化3維地圖的資料
            for (int i = 0; i < map_size; i++)
                for (int j = 0; j < map_size; j++)
                    for (int k = 0; k < map_size; k++)
                        if (rand.NextDouble() > map_density)    //依照牆壁密度建立迷宮
                            map[i, j, k] = 0;   //路 you can pass
                        else
                            map[i, j, k] = grid_hard;   //牆壁血量初始化 you can't pass
        }

        

        public void testRange(Point3D point)
        {
            Console.WriteLine("onEdge:{0} , inRange{1}", point.onEdge(this.range), point.inRange(this.range));
        }
        public int valueAt(Point3D point)
        {
            if (point.onEdge(this.range) || (point.inRange(this.range)))
                return map[point.x, point.y, point.z];
            else
                return 0;

        }
    }
}
