using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class GameState
    {
        public GameState(long t)
        {
            Tick = t;
        }
        public long Tick { get; set; }
        public List<Player> Players = new List<Player>();
        public List<StarDust> Dust = new List<StarDust>();
        public List<Bullet> Bullets = new List<Bullet>();
    }
}
