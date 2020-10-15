using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class Circle
    {
        public Vector2D pos;
        public float r;

        public Circle(Vector2D p, float rad)
        {
            pos = p;
            r = rad;
        }
    }
}
