using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Bosses
{
    public class BossJester : BossEnemy
    {
        private int _healthStage;
        private Vector2 _direction = new(1, 1);
        private float _moveSpeed = 1.3f;
        private bool _blue;
        private bool _aux;
        private float _shotTilt;
        private Vector2 _target;

        protected override void Move()
        {
            var position = transform.position;
            if (BossStep < 0)
            {
                position.y -= 0.01f;
            }
            else
            {
                position += (Vector3)_direction * (0.01f * _moveSpeed);
                if (position.x is > 20 or < -20)
                    _direction.x *= -1;
                if (position.y is > 12 or < -5)
                    _direction.y *= -1;
            }

            transform.position = position;
        }

        protected override void LoseHp(int receivedDamage)
        {
            base.LoseHp(receivedDamage);
            if (hp <= 1500 && _healthStage <= 1)
            {
                //LoadPath("Sprites/boss_jester/destroyed_blue", 484, 254);
                GetComponent<Animator>().SetBool("Destroyed2", true);
                _moveSpeed = 2.2f;
                _healthStage += 1;
            }
            else if (hp <= 3000 && _healthStage <= 0)
            {
                GetComponent<Animator>().SetBool("Destroyed1", true);
                _moveSpeed = 1.8f;
                //LoadPath("Sprites/boss_jester/destroyed_pink", 484, 254);
                _healthStage += 1;
            }
        }

        protected override List<ShipShot> CreateShots()
        {
            var shots = new List<ShipShot>();
            var left = new List<ShipShot>();
            var right = new List<ShipShot>();
            var shotDirection = new Vector2();
            if (BossStep == 0)
            {
                ChangeShot(0.7f, 7, 0);
                shotPrefab = _blue ? shotPrefabs[0] : shotPrefabs[1];
                for (var i = 0; i < 3; i++)
                {
                    for (var j = -1; j <= 1; j += 2)
                    {
                        for (var w = 0; w < 3; w++)
                        {
                            shots.Add(new ShipShot(
                                j * 5 + Mathf.Cos(_shotTilt + i / 10f + Mathf.PI * 2 / 3 * w) * j,
                                Mathf.Sin(_shotTilt + i / 10f + Mathf.PI * 2 / 3 * w),
                                Mathf.Cos(_shotTilt + i / 10f + Mathf.PI * 2 / 3 * w) * j,
                                Mathf.Sin(_shotTilt + i / 10f + Mathf.PI * 2 / 3 * w)));
                        }
                    }
                }

                _blue = !_blue;
                _shotTilt += 0.2f;
            }

            if (BossStep == 1 || BossStep == 3)
            {
                if (BossStep == 1 && _healthStage <= 1)
                {
                    ChangeShot(1f, 15, 2);
                }

                var type = FindObjectsOfType<PlayerShip>();
                if (type.Length < 1) return new List<ShipShot>();
                _target = type[0].transform.position;
                foreach (var t in type)
                {
                    var pos = t.transform.position;
                    if (Vector2.Distance(pos, transform.position) <
                        Vector2.Distance(_target, transform.position))
                        _target = pos;
                }

                var position = transform.position;
                shotDirection = new Vector2(_target.x - position.x, _target.y - position.y);
                shotDirection.Normalize();
                right.Add(new ShipShot(new Vector2(5 + shotDirection.x * 2, shotDirection.y * 2), shotDirection));
                if (BossStep == 1 && _healthStage <= 1)
                {
                    return right;
                }
            }

            if ((BossStep == 2 || BossStep == 3) && _healthStage <= 1)
            {
                var quantity = 6;
                if (BossStep == 2)
                {
                    ChangeShot(0.7f, 15, 3);
                }

                if (_healthStage == 1)
                {
                    quantity = 3;
                    shotPrefab = shotPrefabs[2];
                }

                for (var i = 0; i < quantity; i++)
                {
                    var angle = i * 2 * Mathf.PI / quantity + Mathf.PI / quantity / 2;
                    var shot = new ShipShot(new Vector2(-5 + Mathf.Cos(angle), 1 * Mathf.Sin(angle)),
                        new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)));
                    left.Add(shot);
                }

                if (BossStep == 2)
                {
                    return left;
                }
            }

            if (BossStep == 3)
            {
                _aux = !_aux;
                ChangeShot(0.7f, 15);
                if (_aux && _healthStage <= 1)
                {
                    _blue = !_blue;
                    if (_blue)
                    {
                        shotPrefab = shotPrefabs[2];
                        return right;
                    }

                    shotPrefab = shotPrefabs[3];
                    return left;
                }

                shotPrefab = shotPrefabs[4];
                shots.Add(new ShipShot(new Vector2(), shotDirection));
            }

            return shots;
        }

        public override Round CreateWaves()
        {
            var waves = new List<Wave>();
            switch (BossStep)
            {
                case 1:
                    if (_healthStage <= 1)
                    {
                        waves.Add(new Wave(3, 1, 3, -10, 0));
                        waves.Add(new Wave(3, 1, 4, 10, 0));
                    }
                    else
                    {
                        waves.Add(new Wave(3, 2, 6));
                        waves.Add(new Wave(3, 2, 7));
                        waves.Add(new Wave(2, 6, 7));
                    }

                    break;
                case 2:
                    waves.Add(new Wave(0, 10, 5, 0, -2));
                    waves.Add(new Wave(1, 5, 5));
                    break;
                case 3:
                    if (_healthStage >= 2)
                    {
                        waves.Add(new Wave(2, 2, 3, -10, 0));
                        waves.Add(new Wave(2, 2, 4, 10, 0));
                        waves.Add(new Wave(0, 10, 5));
                    }
                    break;
            }
            return new Round(waves);
        }
    }
}