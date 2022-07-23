using UnityEngine;

namespace Game.Scripts.Gameplay.Pickups
{
	public class AmmoPickup : PickupBase
	{
		[SerializeField]
		private int _ammoAmount = 10;
		protected override PickupType DefaultPickupId => PickupType.Ammo;

		public override void OnPickedUp(IPickupGetter pickupGetter)
		{
			if (pickupGetter.TryTakePickup(new PickupInfo(_ammoAmount, PickupId)))
			{
				Destroy(gameObject);
			}
		}
	}
}