using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies.Bosses
{
    public class BossEnemy : Ship
    {
        public List<Shot> shotPrefabs;
        public int bossCycle = 1;
        protected int BossStep = -1;
        public float bossTimer = 0;
        public bool summon = true;
        private bool _changedShot = false;
        private GameObject _target = null;
        public GameObject bossExplosion;
        public UnityEvent onDefeat;

        protected override void LoseHp(int receivedDamage)
        {
            if (hp - receivedDamage <= 0)
            {
                onDefeat.Invoke();
                Instantiate(bossExplosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity );
                Destroy(gameObject);
            }
            else
                base.LoseHp(receivedDamage);
        }

        protected void ChangeShot(float time, int speed)
        {
            if (_changedShot) return;
            shotTime = time;
            shotSpeed = speed;
            _changedShot = true;
        }
        
        protected void ChangeShot(float time, int speed, int i)
        {
            if (_changedShot) return;
            shotPrefab = shotPrefabs[i];
            ChangeShot(time, speed);
        }

        public virtual Round CreateWaves()
        {
            return new Round(new List<Wave>());
        }

        protected override void Update()
        {
            Shoot();
            base.Update();
            if (bossTimer < 1)
            {
                bossTimer += Time.deltaTime / 15;
            }
            else
            {
                if (BossStep < bossCycle)
                {
                    BossStep += 1;
                }
                else
                {
                    BossStep = 0;
                }

                bossTimer = 0;
                OnStep();
            }
            Move();
        }

        protected virtual void OnStep()
        {
            summon = true;
            _changedShot = false;
            ShootTime = 0;
        }
    }
}