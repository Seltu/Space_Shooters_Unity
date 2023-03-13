using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

public class EnemyShip : Ship
{
    public SpriteShapeController curve;
    private float _bezierTimer;
    private Vector2 _previousPoint;

    protected override void Start()
    {
        gameObject.transform.position = curve.spline.GetPoint(_bezierTimer);
        base.Start();
    }
    protected override void Move()
    {
        var spline = curve.spline;
        gameObject.transform.position = spline.GetPoint(_bezierTimer);
        _bezierTimer += 0.002f;
        if (_bezierTimer <= 1f) return;
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        Shoot();
    }
}