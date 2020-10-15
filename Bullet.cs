using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class Bullet : Circle
    {
        public int ownerId;
        public Vector2D vel;
        public Bullet(float x, float y, int owner) : base(new Vector2D(x, y), Game.BulletRadius)
        {
            ownerId = owner;
        }


        public void Move()
        {
            pos += vel;
        }
    }
}
