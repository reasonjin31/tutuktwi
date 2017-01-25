using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOOK
{
    class Point2D
    {
        public int xPos { get; set; }
        public int yPos { get; set; }


        public Point2D() { }
        public Point2D(int x, int y)
        {
            xPos = x;
            yPos = y;
        }


        public void setPoint2D(int x, int y)
        {
            xPos = x;
            yPos = y;
        }
    }
}
