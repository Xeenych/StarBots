using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLib
{
    public class Game
    {
        const int player_timeout = 100;
        public const int xmax = 300;
        public const int ymax = 300;
        public const float vel_max = 10.0f;
        public const int numstars = 20;
        public const float ShipRadius = 10.0f;
        public const int ShootingCooldown = 10;
        public const float BulletRadius = 2.0f;
        public const float BulletSpeed = 11.0f;
        Dictionary<string, Player> keys_to_players = new Dictionary<string, Player>();
        List<StarDust> startdust = new List<StarDust>();
        List<Bullet> bullets = new List<Bullet>();
        Int64 tick = 0;
        Random r = new Random();
        public Game() {
            for (int i = 0; i < numstars; i++)
                startdust.Add(new StarDust(r.Next(-xmax, xmax), r.Next(-ymax, ymax)));
        }

        public GameInfo MakeNewPlayer()
        {
            GameInfo info = new GameInfo();
            string key = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16);
            int id = r.Next();
            var p = new Player(id, r.Next(-xmax, xmax), r.Next(-ymax, ymax));
            p.last_tick = tick;
            keys_to_players.Add(key, p);
            info.Key = key;
            info.MyId = GetPlayerByKey(key).id;
            info.ShootingCooldown = ShootingCooldown;
            info.ShipRadius = ShipRadius;
            info.MaxSpeed = vel_max;
            return info;
        }

        public int GetPlayerCount()
        {
            return keys_to_players.Count;
        }

        bool PlayerExists(string key)
        {
            return keys_to_players.ContainsKey(key);
        }
        public Player GetPlayerByKey(string key)
        {
            return keys_to_players[key];
        }

        void RemoveTimeoutPlayers()
        {
            List<string> to_remove = new List<string>();
            foreach (var k in keys_to_players.Keys)
            {
                var p = GetPlayerByKey(k);
                if (tick - p.last_tick > player_timeout)
                    to_remove.Add(k);
            }

            foreach (var key in to_remove) {
                keys_to_players.Remove(key);
            }
        }

        void CollectStarDust()
        {
            List<StarDust> to_remove = new List<StarDust>();
            foreach (var d in startdust)
                foreach (var p in keys_to_players.Values)
                {
                    var dist = Math.Sqrt(p.pos.QDistTo(d.pos));
                    if (dist < Math.Max(p.r, d.r))
                        to_remove.Add(d);
                    p.score++;
                }

            foreach (var s in to_remove)
                startdust.Remove(s);
        }

        public void MakeStarDust()
        {
            if (startdust.Count < numstars)
                for (int i = 0; i < numstars - startdust.Count; i++)
                    startdust.Add(new StarDust(r.Next(-xmax, xmax), r.Next(-ymax, ymax)));
        }

        public void ApplyForce(Player c, Vector2D f)
        {
            Vector2D relative = f - c.pos;
            relative.Normalize();
            c.vel += relative;
            if (c.vel.Abs() > vel_max)
            {
                c.vel.Normalize();
                c.vel *= vel_max;
            }

            c.pos += c.vel;

            if (c.pos.x > xmax)
            {
                c.pos.x = xmax;
                c.vel.x = 0;
            }

            if (c.pos.x < -xmax)
            {
                c.pos.x = -xmax;
                c.vel.x = 0;
            }

            if (c.pos.y < -ymax)
            {
                c.pos.y = -ymax;
                c.vel.y = 0;
            }

            if (c.pos.y > ymax)
            {
                c.pos.y = ymax;
                c.vel.y = 0;
            }
        }

        void Shoot(Player p, PlayerAction action)
        {
            if (action.Shoot && p.CanShoot())
            {
                Vector2D loc = p.pos + (p.vel / p.vel.Abs()) * p.r * 2.0f;
                Bullet b = new Bullet(loc.x, loc.y, p.id);
                b.vel = p.vel / p.vel.Abs() * BulletSpeed;
                bullets.Add(b);
                p.Shoot();
            }
            p.DecreaseCooldown();
        }
        void CheckBulletToPlayerCollisions()
        {
            List<Player> players_to_remove = new List<Player>();
            List<Bullet> bullets_to_remove = new List<Bullet>();
            foreach (Bullet b in bullets)
                foreach (Player p in keys_to_players.Values)
                {
                    var dist = Math.Sqrt(p.pos.QDistTo(b.pos));
                    if (dist < Math.Max(p.r, b.r)) {
                        bullets_to_remove.Add(b);
                        players_to_remove.Add(p);
                    }
                }

            foreach (var s in players_to_remove)
            {
                var key = keys_to_players.FirstOrDefault(o => { return o.Value.id == s.id; }).Key;
                keys_to_players.Remove(key);
            }

            foreach (var b in bullets_to_remove)
                bullets.Remove(b);
        }

        void RemoveBullets()
        {
            bullets.RemoveAll(o => { return o.pos.Abs() > 1000.0f; });
        }
        public void Update(IEnumerable<PlayerAction> acts)
        {
            tick++;

            foreach (Bullet b in bullets)
            {
                b.Move();
            }

            foreach (var action in acts)
            {
                if (action == null)
                    continue;
                if (action.key == null)
                    continue;
                if (!PlayerExists(action.key))
                    continue;

                var p = GetPlayerByKey(action.key);

                ApplyForce(p, action.force);
                Shoot(p, action);
                p.last_tick = tick;
            }

            CollectStarDust();
            CheckBulletToPlayerCollisions();
            MakeStarDust();
            RemoveTimeoutPlayers();
            RemoveBullets();
        }
        public GameState GetState()
        {
            var gs = new GameState(tick);

            foreach (var p in keys_to_players.Values)
                gs.Players.Add(p);


            foreach (var b in bullets)
                gs.Bullets.Add(b);

            foreach (var s in startdust)
                gs.Dust.Add(s);

            return gs;
        }
    }



}
