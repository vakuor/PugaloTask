using UnityEngine;

namespace Game.Scripts.Gameplay.Pickups
{
	[RequireComponent(typeof(Collider))]
	public abstract class PickupBase : MonoBehaviour
	{
		[SerializeField]
		private PickupType _pickupId;
		public PickupType PickupId => _pickupId;
		protected abstract PickupType DefaultPickupId { get; }
		
		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IPickupGetter pickupGetter))
			{
				OnPickedUp(pickupGetter);
			}
		}
		
		public abstract void OnPickedUp(IPickupGetter pickupGetter);
		
		private void OnValidate()
		{
			bool hasCollider = TryGetComponent(out Collider coll);
			if (!hasCollider)
			{
				UnityEngine.Debug.LogError(gameObject.name + " doesn't have a collider attached!");
				return;
			}
			if (!coll.isTrigger)
			{
				UnityEngine.Debug.LogError(gameObject.name + " collider is not trigger type!");
			}
		}

		protected virtual void Reset()
		{
			bool hasCollider = TryGetComponent(out Collider coll);
			if (hasCollider && !coll.isTrigger)
			{
				coll.isTrigger = true;
				UnityEngine.Debug.Log("Collider type of " + name + " set to trigger type");
			}
			_pickupId = DefaultPickupId;
		}
	}
}