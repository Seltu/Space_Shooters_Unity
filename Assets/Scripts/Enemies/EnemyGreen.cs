using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyGreen : EnemyShip
    {
        private Vector2 _target;

        protected override List<ShipShot> CreateShots()
        {
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
            var shotDirection = new Vector2(_target.x - position.x, _target.y - position.y);
            shotDirection.Normalize();
            var shots = new List<ShipShot>
            {
                new(shotDirection*2, shotDirection)
            };
            return shots;
        }
    }
}
