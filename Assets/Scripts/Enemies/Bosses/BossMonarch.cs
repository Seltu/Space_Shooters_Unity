using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Bosses
{
    public class BossMonarch : BossEnemy
    {
            private Vector2 _direction = new(1, 1);
            private float _moveSpeed = 1.3f;
            private bool _entered;
            private float _shotTilt;

            protected override void Move()
            {
                var newColor = Renderer.color;
                if (bossTimer > 0.9)
                {
                    newColor.a = (1 - (bossTimer - 0.9f) * 10);
                }
                else if (bossTimer < 0.2)
                {
                    var alpha = 1 - (1.1f - bossTimer * 11);
                    if (alpha > 0)
                    {
                        newColor.a =  alpha;
                    }
                    else
                    {
                        newColor.a = 0;
                    }
                }
                else
                {
                    newColor.a = 255;
                }
                Renderer.color = newColor;
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
                    if (position.y is > 12 or < 0)
                        _direction.y *= -1;
                }

                transform.position = position;
            }

            protected override List<ShipShot> CreateShots()
            {
                var shots = new List<ShipShot>();
                var extents = Renderer.bounds.extents.x;
                if (BossStep == 0 || BossStep == 5)
                {
                    var quantity = 0;
                    if (BossStep == 0)
                    {
                        ChangeShot(0.2f, 15, 0);
                        quantity = 7;
                    }
                    else
                    {
                        ChangeShot(1f, 15, 0);
                        quantity = 14;
                    }

                    for (int i = 0; i < quantity; i++)
                    {
                        float angle = i * 2 * Mathf.PI / quantity + 0.5f * Mathf.PI / quantity + _shotTilt;
                        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        shots.Add(new ShipShot(Mathf.Cos(angle), Mathf.Sin(angle), direction.x, direction.y));
                    }

                    _shotTilt += Mathf.PI / quantity;
                }
                else if (BossStep == 1)
                {
                    ChangeShot(0.1f, speed: 20);
                    int quantity = 7;

                    for (int i = 0; i < quantity; i++)
                    {
                        float angle = i * 2 * Mathf.PI / quantity + 0.5f * Mathf.PI / quantity + _shotTilt;
                        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        shots.Add(new ShipShot(Mathf.Cos(angle), Mathf.Sin(angle), direction.x, direction.y));
                    }

                    if (0 < bossTimer && bossTimer < 0.1f || 0.2f < bossTimer && bossTimer < 0.3f || 0.4f < bossTimer && bossTimer < 0.5f
                        || 0.6f < bossTimer && bossTimer < 0.7f || 0.8f < bossTimer && bossTimer < 0.9f)
                    {
                        _shotTilt += 0.05f;
                    }
                    else
                    {
                        _shotTilt -= 0.05f;
                    }
                }
                else if (BossStep == 2)
                {
                    ChangeShot(3, 3, 2);
                    int quantity = 7;
                    for (int i = 0; i < quantity; i++)
                    {
                        float angle = i * 2 * Mathf.PI / quantity + 0.5f * Mathf.PI / quantity;
                        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        shots.Add(new ShipShot(Mathf.Cos(angle), Mathf.Sin(angle), direction.x, direction.y));
                    }
                }
                else if (BossStep == 3)
                {
                    ChangeShot(1.5f, 10, 1);
                    var type = FindObjectsOfType<PlayerShip>();
                    if (type.Length < 1) return new List<ShipShot>();
                    var target = type[0].transform.position;
                    foreach (var t in type)
                    {
                        var pos = t.transform.position;
                        if (Vector2.Distance(pos, transform.position) <
                            Vector2.Distance(target, transform.position))
                            target = pos;
                    }
                    var position = transform.position;
                    var shotDirection = new Vector2(target.x - position.x, target.y - position.y);
                    shotDirection.Normalize();
                    shots.Add(new ShipShot(new Vector2(shotDirection.x * 2, shotDirection.y * 2), shotDirection));
                }
                return shots;
            }

            public override Round CreateWaves()
            {
                var waves = new List<Wave>();
                switch (BossStep)
                {
                    case 4:
                        waves.Add(new Wave(0, 6, 5));
                        waves.Add(new Wave(4, 6, 5, 0, -1));
                        waves.Add(new Wave(2, 6, 5, 0, -2));
                        waves.Add(new Wave(1, 6, 5, 0, -3));
                        break;
                    case 5:
                        waves.Add(new Wave(5, 6, 6));
                        waves.Add(new Wave(5, 6, 7));
                        break;
                }
                return new Round(waves);
            }
            
            protected override void OnStep()
            {
                base.OnStep();
                if (_entered)
                {
                    var newPos = new Vector2(Random.Range(-18, 18), Random.Range(-2, 12));
                    transform.position = newPos;
                }
                else
                {
                    _entered = true;
                }
                _shotTilt = 0;
                _moveSpeed = BossStep switch
                {
                    0 => 3,
                    1 => 1,
                    _ => _moveSpeed
                };
            }
    }
    }
