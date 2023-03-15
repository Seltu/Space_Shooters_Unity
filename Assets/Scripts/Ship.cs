using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int damage = 1;
    public int maxHp = 1;
    public int hp = 1;
    public float shotSpeed = 4f;
    public float shotTime = 0f;
    public GameObject shootPoint;
    public Shot shotPrefab;
    public bool _shoot = false;
    public int team = 0;
    protected SpriteRenderer Renderer;
    protected float ShootTime = 0f;

    protected virtual void Start()
    {
        hp = maxHp;
        Renderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Shoot()
    {
        if(ShootTime <= 0)
            _shoot = true;
    }

    protected virtual void LoseHp(int receivedDamage)
    {
        hp -= receivedDamage;
        if (hp > 0) return;
        Destroy(gameObject);
        // explosionSoundEffect.Play();
    }
    
    protected virtual void Move()
    {
        // Implementation for movement goes here
    }
    
    protected virtual List<ShipShot> CreateShots()
    {
        // Implementation for creating shots goes here
        return null;
    }

    protected virtual void Update()
    {
        if (ShootTime > 0)
        {
            ShootTime -= Time.deltaTime;
        }
        if (!_shoot || !(ShootTime <= 0)) return;
        ShootTime = shotTime;
        var shots = CreateShots();
        if (shots != null)
        {
            foreach (var instruction in shots)
            {
                var shot = Instantiate(shotPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
                shot.SetShot(this, instruction);
            }
        }
        _shoot = false;
        // shot_sound_effect.Play();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shot"))
        {
            var shot = other.GetComponent<Shot>();
            var ship = shot.Ship;
            if (ship.team == team) return;
            LoseHp(ship.damage);
            Destroy(other.gameObject); // to add shot destruction method that also creates explosion in it's place
        }
    }
}