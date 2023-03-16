namespace Pickups
{
    public class HealthPickup : Pickup
    {
        protected override void Effect(PlayerShip ship)
        {
            if (ship.hp < ship.maxHp)
                ship.hp += 1;
        }
    }
}
