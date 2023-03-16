using System.Collections;
using UnityEngine;

namespace Pickups
{
    public class Pickup : MonoBehaviour
    {
        private void Update()
        {
            transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
        }

        protected virtual void Effect(PlayerShip ship) {}
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            var player = other.GetComponent<PlayerShip>();
            Effect(player);
            Destroy(gameObject);
        }
    }

    public class TemporaryPickup : Pickup
    {
        public float time;

        protected override void Effect(PlayerShip ship)
        {
            ship.StartCoroutine(Wear(ship));
        }
        private IEnumerator Wear(PlayerShip ship)
        {
            yield return new WaitForSeconds(time);
            WearOut(ship);
        }
        protected virtual void WearOut(PlayerShip ship){}
    }
}