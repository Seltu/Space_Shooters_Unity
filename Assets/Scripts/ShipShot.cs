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
    public ShipShot(float offsetX, float offsetY, float velocityX, float velocityY)
    {
        Offset = new Vector2(offsetX, offsetY);
        Velocity = new Vector2(velocityX, velocityY);
    }
}
