using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyBlue : EnemyShip
    {
        private float _shotTilt = 0;
        protected override List<ShipShot> CreateShots()
        {
            const int quantity = 6;
            var shots = new List<ShipShot>();
            for (var i = 0; i < quantity; i++)
            {
                var angle = i * 2 * Math.PI / quantity + _shotTilt;
                var shot = new ShipShot(new Vector2( (float)(1 * Math.Cos(angle)), (float)(1 * Math.Sin(angle))),
                    new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
                shots.Add(shot);
            }
            _shotTilt += (float) Math.PI / quantity;
            return shots;
        }
    }
}
