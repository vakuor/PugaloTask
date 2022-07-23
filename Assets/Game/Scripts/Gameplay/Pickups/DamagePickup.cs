using UnityEngine;

namespace Game.Scripts.Gameplay.Pickups
{
    public class DamagePickup : PickupBase
    {
        [SerializeField]
        private int _damageAmount = 10;
        protected override PickupType DefaultPickupId => PickupType.Damage;

        public override void OnPickedUp(IPickupGetter pickupGetter)
        {
            pickupGetter.TryTakePickup(new PickupInfo(_damageAmount, PickupId));
        }
    }
}
