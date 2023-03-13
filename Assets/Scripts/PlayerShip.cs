using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public int score = 0;
    public  float moveSpeed;
    public  int numberOfShots;
    public int invincibilityTime;
    public SpriteRenderer spriteRenderer;
    private int _invincibleTimer = 0;
    private Rigidbody2D _rb;
    private ButtonAxis _vertical = new ButtonAxis(KeyCode.W, KeyCode.S);
    private ButtonAxis _horizontal = new ButtonAxis(KeyCode.D, KeyCode.A);

    public PlayerShip()
    {
        score = 0;
        shotTime = 10f;
        numberOfShots = 1;
        shotSpeed = 10f;
        moveSpeed = 6f;
        damage = 10;
        maxHp = 5;
        hp = 5;
        invincibilityTime = 100;
        _invincibleTimer = 0;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected override void LoseHp(int receivedDamage)
    {
        if (_invincibleTimer > 0) return;
        base.LoseHp(receivedDamage);
        _invincibleTimer = invincibilityTime;
    }

    private float ShotAngle(int i)
    {
        float spreadAngle = Mathf.PI / 18f * numberOfShots;
        if (numberOfShots > 1)
        {
            return i * spreadAngle / (numberOfShots - 1) - Mathf.PI / 2f - spreadAngle / 2f;
        }
        else
        {
            return -Mathf.PI / 2f;
        }
    }
    
    protected override List<ShipShot> CreateShots()
    {
        List<ShipShot> shots = new List<ShipShot>();
        for (var i = 0; i < numberOfShots; i++)
        {
            var angle = ShotAngle(i);
            var dir = new Vector2(Mathf.Cos(angle), -Mathf.Sin(angle));
            var position = new Vector2( Mathf.Cos(angle), 0);
            ShipShot shot = new ShipShot();
            shot.Offset = position;
            shot.Velocity = dir;
            shots.Add(shot);
        }
        return shots;
    }

    protected override void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            Shoot();
        _vertical.Check();
        _horizontal.Check();
        var newColor = spriteRenderer.color;
        newColor.a =  (255 - 255 * _invincibleTimer / 100);
        spriteRenderer.color = newColor;
        base.Update();
    }

    protected override void Move()
    {
        var movement = Vector2.zero;
        movement.x = _horizontal.GetAxis();
        movement.y = _vertical.GetAxis();
        _rb.velocity = movement.normalized * moveSpeed;
    }
}