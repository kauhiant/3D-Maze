using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My3DMaze
{
    static class Const
    {
        static public Range3D mazeRange;//需要初始化，每改一次地圖大小要重設一次
        static public MapGraph mainGraph;
        static public int seenSize = 8;
    }
}
