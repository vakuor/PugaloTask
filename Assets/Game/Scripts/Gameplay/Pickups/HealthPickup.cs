using UnityEngine;

namespace Game.Scripts.Gameplay.Pickups
{
	public class HealthPickup : PickupBase
	{
		[SerializeField]
		private int _healthAmount = 10;
		protected override PickupType DefaultPickupId => PickupType.Heal;

		public override void OnPickedUp(IPickupGetter pickupGetter)
		{
			if(pickupGetter.TryTakePickup(new PickupInfo(_healthAmount, PickupId)))
			{
				Destroy(gameObject);
			}
		}
	}
}