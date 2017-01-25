using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOOK
{
    class Vector3D
    {
        public int xDir { get; set; }
        public int yDir { get; set; }
        public int zDir { get; set; }


        public Vector3D() { }
        public Vector3D(int x, int y, int z)
        {
            xDir = x;
            yDir = y;
            zDir = z;
        }


        public void setVector3D(int x, int y, int z)
        {
            xDir = x;
            yDir = y;
            zDir = z;
        }
    }
}
