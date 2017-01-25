using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOOK
{
    class Polygon
    {
        private Point3D[] p;
        //private Vector3D normal;


        public Polygon()
        {
            p = new Point3D[3];
        }

        public Polygon(Point3D p1, Point3D p2, Point3D p3)
        {
            p = new Point3D[3];
            p[0] = p1;
            p[1] = p2;
            p[2] = p3;
        }


        public Point3D getP1()
        {
            return p[0];
        }

        public Point3D getP2()
        {
            return p[1];
        }

        public Point3D getP3()
        {
            return p[2];
        }

        //public Vector3D getNormal()
        //{
        //    return normal;
        //}

        public void setPolygon(Point3D p1, Point3D p2, Point3D p3)
        {
            p[0] = p1;
            p[1] = p2;
            p[2] = p3;
        }


        /* Function
        public void computeNormal() {
            Vector3D a = new Vector3D( p[1].getX()-p[0].getX(), p[1].getY()-p[0].getY(), p[1].getZ()-p[0].getZ() );
            Vector3D b = new Vector3D( p[2].getX() - p[0].getX(), p[2].getY() - p[0].getY(), p[2].getZ() - p[0].getZ() );

            normal.setVector3D( a.getY()*b.getZ()-a.getZ()*b.getY(),
                                a.getZ()*b.getX()-a.getX()*b.getZ(),
                                a.getX()*b.getY()-a.getY()*b.getX() );
        }
        */
    }
}
