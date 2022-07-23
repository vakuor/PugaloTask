using System;

namespace Game.Scripts.Gameplay
{
	public interface IPickupGetter
	{
		public event EventHandler<PickupInfo> OnPickupGet;
		public bool TryTakePickup(PickupInfo pickupInfo);
	}

	public class PickupInfo : EventArgs
	{
		public int Count { get; }
		public PickupType PickupType { get; }

		public PickupInfo(int count, PickupType pickupType)
		{
			Count = count;
			PickupType = pickupType;
		}
	}

	public enum PickupType
	{
		Heal,
		Ammo,
		Damage
	}
}