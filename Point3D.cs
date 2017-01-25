using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOOK
{
    class Point3D
    {
        public int xPos { get; set; }
        public int yPos { get; set; }
        public int zPos { get; set; }


        public Point3D() { }
        public Point3D(int x, int y, int z)
        {
            xPos = x;
            yPos = y;
            zPos = z;
        }


        public void setPoint3D(int x, int y, int z)
        {
            xPos = x;
            yPos = y;
            zPos = z;
        }
    }
}
