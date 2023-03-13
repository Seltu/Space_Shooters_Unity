using System.Collections.Generic;
using UnityEngine;

public class BaronMinion : EnemyShip
{
    protected override List<ShipShot> CreateShots()
    {
        var shots = new List<ShipShot>
        {
            new(Vector2.zero, new Vector2(1, -1)),
            new(Vector2.zero, new Vector2(-1, -1))
        };
        return shots;
    }
}
