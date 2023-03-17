using UnityEngine;
using UnityEngine.Serialization;

public class Shot : MonoBehaviour
{
    // [NonSerialized]
    [FormerlySerializedAs("Ship")] public Ship ship;
    protected Vector2 Vel;
    public float timeToDisappear;

    public void SetShot(Ship ship, ShipShot shot)
    {
        this.ship = ship;
        transform.position += new Vector3(shot.Offset.x, shot.Offset.y, 0);
        Vel = new Vector2(shot.Velocity.x  * ship.shotSpeed, shot.Velocity.y * ship.shotSpeed);
    }

    private void Start()
    {
        Destroy(gameObject, timeToDisappear);
    }

    private void Update()
    {
        Move();
        Rotation();
    }

    private void Rotation()
    {
        var vel = Vel;
        vel.Normalize();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, -vel);
    }

    protected virtual void Move()
    {
        transform.position += (Vector3) Vel * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}