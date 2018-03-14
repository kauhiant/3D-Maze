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
        private int[,,] map;        //存牆壁的血量 3維地圖 the data of 3dMap

        public int map_size  { get; private set; }       //地圖大小 正方形地圖 非圖片大小(64-256)
        public int grid_hard { get; private set; }       //牆壁厚度(血量) the HP of gridWall
        public Range3D range { get; private set; }

        // random create a 3D-Map by density
        // set max of each grid with wallHard.
        public Map3D(int mapSize,int wallHard, double density)
        {
            map_size = mapSize;
            grid_hard = wallHard;
            range = new Range3D(new Range1D(map_size), new Range1D(map_size), new Range1D(map_size));

            //Create data of 3D Map and Initiate it
            //--創造3維地圖-- 
            map = new int[map_size, map_size, map_size];
            //初始化3維地圖的資料
            for (int i = 0; i < map_size; i++)
                for (int j = 0; j < map_size; j++)
                    for (int k = 0; k < map_size; k++)
                        if (rand.NextDouble() > density)    //依照牆壁密度建立迷宮
                            map[i, j, k] = 0;   //路 you can pass
                        else
                            map[i, j, k] = grid_hard;   //牆壁血量初始化 you can't pass
        }

        // set max of each grid with wallHard.
        // assign reference-Map to 3D-Map.
        public Map3D(int[,,] referenceMap, int wallHard)
        {
            this.map = referenceMap;
            this.grid_hard = wallHard;
            this.map_size = referenceMap.GetLength(0);
            this.range = new Range3D(new Range1D(map_size), new Range1D(map_size), new Range1D(map_size));
        }

        // set value at
        public void setValueAt(Point3D location,int value)
        {
            if (location.inRange(this.range))
                map[location.x, location.y, location.z] = value;
        }

        // get value at point.
        public int valueAt(Point3D point)
        {
            if (point.inRange(this.range))
                return map[point.x, point.y, point.z];
            else
                return -1;
        }

        // grid at point be attacked.
        public void beAttack(Point3D target,int destroy)
        {
            if (!target.inRange(range)) return;
            if (valueAt(target) <= 0) return;

            if ((this.map[target.x, target.y, target.z] -= destroy) < 0)
                this.map[target.x, target.y, target.z] = 0;
        }

        // create a 2D-Map by the plane of this 3D-Map.
        public Map2D creat2DMapOn(Plane plane)
        {
            Map2D returnMap = new Map2D(this.map_size);
            fix2DMapOn(returnMap, plane);
            return returnMap;
        }

        // fix the 2D-Map by the plane of this 3D-Map.
        // shouldn't recreate 2D-Map
        public void fix2DMapOn(Map2D map, Plane plane)
        {
            Point3D point = Point3D.createPointOnPlane(plane);
            Point2D iterator = new Point2D(point, plane.dimension);

            for (int i = 0; i < map_size; i++)
                for (int j = 0; j < map_size; j++)
                {
                    iterator.moveTo(i, j);
                    map.setGrid(i, j, this.valueAt(point));
                }
        }

    }
}
