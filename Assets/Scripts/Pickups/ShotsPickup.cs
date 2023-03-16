namespace Pickups
{
    public class ShotsPickup : TemporaryPickup
    {
        protected override void Effect(PlayerShip ship)
        {
            ship.numberOfShots += 1;
            base.Effect(ship);
        }
        protected override void WearOut(PlayerShip ship)
        {
            ship.numberOfShots -= 1;
        }
    }
}
