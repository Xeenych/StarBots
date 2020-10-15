using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class StarDust : Circle
    {
        public StarDust(float x, float y) : base(new Vector2D(x,y), 2.0f)
        {
        }
    }
}
