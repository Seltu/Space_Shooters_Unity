using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyShip : Ship
{
    public SpriteShapeController curve;
    private float _bezierTimer;
    private Vector2 _previousPoint;
    private Vector2 _offset;

    protected override void Start()
    {
        gameObject.transform.position = curve.spline.GetPoint(_bezierTimer);
        base.Start();
    }
    protected override void Move()
    {
        var spline = curve.spline;
        gameObject.transform.position = spline.GetPoint(_bezierTimer)+_offset;
        _bezierTimer += 0.002f;
        if (_bezierTimer <= 1f) return;
        Destroy(gameObject);
    }

    protected override List<ShipShot> CreateShots()
    {
        List<ShipShot> shots = new List<ShipShot>();
        var dir = new Vector2(0, -1);
        var position = new Vector2( 0, 0);
        ShipShot shot = new ShipShot();
        shot.Offset = position;
        shot.Velocity = dir;
        shots.Add(shot);
        return shots;
    }
    
    protected override void Update()
    {
        base.Update();
        Shoot();
    }

    public void AddDelay(float delay)
    {
        ShootTime += delay;
    }
    
    public void SetOffset(Vector2 offset)
    {
        _offset = offset;
    }
}