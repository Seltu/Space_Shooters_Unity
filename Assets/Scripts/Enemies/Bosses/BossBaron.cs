using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Bosses
{
    public class BossBaron : BossEnemy
    {
        private int _direction = 1;
        private int _aux = 1;

        protected override void Move()
        {
            var position = transform.position;
            if (BossStep < 0)
            {
                position.y -= 0.01f;
            }
            else
            {
                position.x += 0.01f * _direction;
            }

            if (position.x > 16)
            {
                _direction = -1;
            }
            else if (position.x < -16)
            {
                _direction = 1;
            }

            if (position.x is > 16 or < -16)
            {
                position.y -= 0.01f * _direction;
            }

            transform.position = position;
        }

        protected override List<ShipShot> CreateShots()
        {
            var shots = new List<ShipShot>();
            var extents = Renderer.bounds.extents.x;
            switch (BossStep)
            {
                case 0:
                    ChangeShot(0.2f, 10, 0);
                    shots.Add(new ShipShot(Random.Range(-extents, extents), 0, 0, -1));
                    break;
                case 1:
                {
                    for (int i = -1; i <= 1; i+=2)
                    {
                        shots.Add(new ShipShot(i*extents, 0, 0, -1));
                    }
                    break;
                }
                case 2:
                {
                    ChangeShot(1f, 15, 1);
                    for (int i = 0; i < 6; i++)
                    {
                        var angle = -Mathf.Cos(Mathf.PI + Mathf.PI * (extents - (i*extents/5)) / extents);
                        shots.Add(new ShipShot(i*extents/3-extents, 0, angle, -1));
                    }

                    break;
                }
                case 3:
                    ChangeShot(0.2f, 15, 1);
                    shots.Add(new ShipShot(0, 0, 0, -1));
                    break;
                case 4:
                {
                    ChangeShot(0.6f, 10, 0);
                    for (int i = 0; i < 4; i++)
                    {
                        shots.Add(new ShipShot(i*extents/2-extents - extents/2 * _aux, 0, 0, -1));
                    }
                    _aux *= -1;
                    break;
                }
            }
            return shots;
        }

        public override Round CreateWaves()
        {
            var waves = new List<Wave>();
            switch (BossStep)
            {
                case 1:
                    waves.Add(new Wave(2, 3, 3, -10, 0));
                    waves.Add(new Wave(2, 4, 4, 10, 0));
                    break;
                case 2:
                    waves.Add(new Wave(4, 4, 1, -10, 0));
                    waves.Add(new Wave(4, 4, 2, 5,  0));
                    break;
                case 3:
                    waves.Add(new Wave(4, 2, 6));
                    waves.Add(new Wave(4, 2, 7));
                    waves.Add(new Wave(4, 2, 6, 0, 2));
                    waves.Add(new Wave(4, 2, 7, 0, 2));
                    waves.Add(new Wave(4, 2, 6, 0, -2));
                    waves.Add(new Wave(4, 2, 7, 0, -2));
                    break;
                case 4:
                    waves.Add(new Wave(4, 6, 5));
                    break;
            }
            return new Round(waves);
        }
    }
}
