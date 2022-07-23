using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Base
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Collider))]
	public abstract class Item : MonoBehaviour
	{
		[SerializeField]
		protected Transform _transform;
		[SerializeField]
		protected Rigidbody _rigidbody;
		public Transform Transform => _transform;

		public virtual bool CanBeUsed() => true;
		public abstract bool TryUse();

		public event EventHandler<AttachEventArgs> OnAttached;
		public event EventHandler<AttachEventArgs> OnDetached;
		
		public void Attach(Transform to)
		{
			SetPhysicsActive(false);
			_transform.parent = to;
			_transform.localPosition = Vector3.zero;
			_transform.localRotation = Quaternion.identity;
			OnAttached?.Invoke(this, new AttachEventArgs{Item = this, To = to});
		}
		
		public void Detach()
		{
			_transform.parent = null;
			SetPhysicsActive(true);
			OnDetached?.Invoke(this, new AttachEventArgs{Item = this});
		}

		public void SetPhysicsActive(bool active) => _rigidbody.isKinematic = !active;
		public void ThrowPhysically(Vector3 from, Vector3 direction, float force)
		{
			Detach();
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			_rigidbody.position = from;
			SetPhysicsActive(true);
			_rigidbody.AddForce(direction.normalized * force);
		}

		protected virtual void Reset()
		{
			if (_transform == null)
			{
				_transform = GetComponent<Transform>();
			}
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
				_rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
			}
		}
	}

	public class AttachEventArgs : EventArgs
	{
		public Item Item { get; set; }
		public Transform To { get; set; }
	}
}