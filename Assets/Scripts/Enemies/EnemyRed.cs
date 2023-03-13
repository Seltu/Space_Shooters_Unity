using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyRed : EnemyShip
    {
        protected override List<ShipShot> CreateShots()
        {
            var shots = new List<ShipShot>
            {
                new(new Vector2(-1.5f, 0), new Vector2(0, -1)),
                new(new Vector2(1.5f, 0), new Vector2(0, -1))
            };
            return shots;
        }
    }
}
