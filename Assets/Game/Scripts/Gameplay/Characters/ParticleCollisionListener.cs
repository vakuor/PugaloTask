using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters
{
	[RequireComponent(typeof(ParticleSystem))]
	public class ParticleCollisionListener : MonoBehaviour
	{
		[SerializeField]
		private ParticleSystem _particleSystem;
		public ParticleSystem ParticleSystem => _particleSystem;
		public event EventHandler<ParticleCollisionEventArgs> OnParticleCollide;
		private void OnParticleCollision(GameObject other)
		{
			OnParticleCollide?.Invoke(this, new ParticleCollisionEventArgs(other));
		}

		private void Reset()
		{
			_particleSystem = GetComponent<ParticleSystem>();
		}
	}

	public class ParticleCollisionEventArgs : EventArgs
	{
		public GameObject Other { get; set; }

		public ParticleCollisionEventArgs(GameObject other)
		{
			Other = other;
		}
	}
}