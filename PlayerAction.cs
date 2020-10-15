using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class PlayerAction
    {
        public PlayerAction(string key, Vector2D f)
        {
            this.key = key;
            force = f;
        }
        public string key { get; set; }
        public Vector2D force { get; set; }
        public bool Shoot { get; set; }
    }
}
