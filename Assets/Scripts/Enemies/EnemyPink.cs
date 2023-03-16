using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyPink : EnemyShip
    {
        private float _shotTilt;
        protected override List<ShipShot> CreateShots()
        {
            var shots = new List<ShipShot>();
            const double quantity = 5;
            var angleIncrement = 2 * Math.PI / quantity;
            for (var i = 0; i < quantity; i++)
            {
                var x = 2 * Math.Cos(i * angleIncrement + _shotTilt);
                var y = 2 * Math.Sin(i * angleIncrement + _shotTilt);
                var dx = Math.Cos(i * angleIncrement + _shotTilt);
                var dy = Math.Sin(i * angleIncrement + _shotTilt);
                var position = new Vector2((float)(x), (float)(y));
                var direction = new Vector2((float)dx, (float)dy);
                var shot = new ShipShot(position, direction * shotSpeed);
                shots.Add(shot);
            }

            if (transform.position.x > 0)
                _shotTilt += 0.3f;
            else
                _shotTilt -= 0.3f;
            return shots;
        }
    }
}
