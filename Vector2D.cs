using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib {

    [System.Serializable]
    public struct Vector2D
    {
        public Vector2D(float _x, float _y) { x = _x; y = _y; }
        public float x;
        public float y;

        public override string ToString()
        {
            return String.Format("[{0}, {1}]", x, y);
        }

        public float QDistTo(Vector2D to) 
        {
            return (to.x - x) * (to.x - x) + (to.y - y) * (to.y - y);
        }

        public float Abs()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }
        public void Normalize()
        {
            if (x == 0 && y == 0)
                return;
            float q = (float)Math.Sqrt(x * x + y * y);
            x = x / q;
            y = y / q;
        }

        static public Vector2D operator+(Vector2D l, Vector2D r)
        {
            return new Vector2D(r.x + l.x, r.y + l.y);
        }

        static public Vector2D operator *(Vector2D l, float k)
        {
            return new Vector2D(k * l.x, k * l.y);
        }
        static public Vector2D operator *(float k, Vector2D l)
        {
            return new Vector2D(k * l.x, k * l.y);
        }

        static public Vector2D operator /(Vector2D l, float k)
        {
            return new Vector2D(l.x/k, l.y/k);
        }

        static public Vector2D operator -(Vector2D l, Vector2D r)
        {
            return new Vector2D(l.x - r.x, l.y - r.y);
        }
    }
}
