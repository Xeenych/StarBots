using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class Player : Circle
    {
        public Vector2D vel;
        int CoolDown = 0;

        public void DecreaseCooldown()
        {
            if (CoolDown > 0)
                CoolDown--;
        }

        public void Shoot()
        {
            CoolDown = Game.ShootingCooldown;
        }

        public bool CanShoot()
        {
            if (CoolDown == 0)
                return true;
            else
                return false;
        }


        public Player(int id, float x, float y):base(new Vector2D(x,y), Game.ShipRadius) {
            this.id = id;
        }
        public int id;
        public Int64 last_tick;
        public int score = 0;
        public float Angle()
        {
            if (vel.x != 0.0)
                return (float)Math.Atan(vel.y / vel.x);
            else
                if (vel.y > 0.0)
                return (float)Math.PI/2.0f;
            else
                return -(float)Math.PI/2.0f;

        }
    }
}
