using UnityEngine;

public struct ShipShot
{
    public Vector2 Offset;
    public Vector2 Velocity;

    public ShipShot(Vector2 offset, Vector2 velocity)
    {
        Offset = offset;
        Velocity = velocity;
    }
}
