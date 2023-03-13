using UnityEngine;

public class Shot : MonoBehaviour
{
    // [NonSerialized]
    public Ship Ship;
    private Vector2 _vel;

    public void SetShot(Ship ship, ShipShot shot)
    {
        Ship = ship;
        transform.position += new Vector3(shot.Offset.x, shot.Offset.y, 0);
        _vel = new Vector2(shot.Velocity.x  * ship.shotSpeed, shot.Velocity.y * ship.shotSpeed);
    }

    private void Start()
    { 
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += (Vector3) _vel * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}