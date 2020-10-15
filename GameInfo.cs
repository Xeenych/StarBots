using System;
using System.Collections.Generic;
using System.Text;

namespace GameLib
{
    public class GameInfo
    {
        public string Version { get; } = "0.1";
        public string Key { get; set; }
        public int MyId { get; set; }
        public float ShipRadius { get; set; }
        public float MaxSpeed { get; set; }
        public int ShootingCooldown { get; set; }
    }
}
