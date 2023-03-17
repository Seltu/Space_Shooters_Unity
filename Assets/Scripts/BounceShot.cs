using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceShot : Shot
{
    public int bounce;
    protected override void Move()
    {
        var position = transform.position;
        if (position.x is > 25 or < -25)
            Vel.x *= -1;
        if (position.y is > 13 or < -13)
            Vel.y *= -1;
        base.Move();
    }
}
