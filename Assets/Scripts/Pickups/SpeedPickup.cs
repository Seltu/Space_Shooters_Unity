using UnityEngine;

namespace Pickups
{
    public class SpeedPickup : TemporaryPickup
    {
        protected override void Effect(PlayerShip ship)
        {
            ship.moveSpeed += 10;
            base.Effect(ship);
        }

        protected override void WearOut(PlayerShip ship)
        {
            ship.moveSpeed -= 10;
        }
    }
}
