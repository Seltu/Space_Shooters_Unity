namespace Pickups
{
    public class ShotSpeedPickup : TemporaryPickup
    {
        protected override void Effect(PlayerShip ship)
        {
            if(ship.shotTime > 0.1f)
                ship.shotTime = 0.1f;
            base.Effect(ship);
        }
        protected override void WearOut(PlayerShip ship)
        {
            ship.shotTime = 0.2f;
        }
    }
}
