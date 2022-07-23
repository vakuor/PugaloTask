using Game.Scripts.Gameplay.Abstractions;
using Game.Scripts.Gameplay.Characters;
using Game.Scripts.Gameplay.Modifiers;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Stones
{
	public class FireStone : StoneItemBase
	{
		[SerializeField]
		private ParticleCollisionListener _particleCollisionListener;

		private void OnEnable() => _particleCollisionListener.OnParticleCollide += OnParticleCollide;
		private void OnDisable() => _particleCollisionListener.OnParticleCollide -= OnParticleCollide;

		private void OnParticleCollide(object sender, ParticleCollisionEventArgs e)
		{
			bool suitableOther = e.Other.TryGetComponent(out DamageableListener pugaloView);
			if (!suitableOther)
			{
				return;
			}
			pugaloView.TakeDamage(this, new DamageArgs { Damage = 1, Modifier = new FireModifier() });
		}
		
		protected override void Use()
		{
			base.Use();
			_particleCollisionListener.ParticleSystem.Play();
		}

		private void OnValidate()
		{
			if (_particleCollisionListener == null)
			{
				Debug.LogError("Particle listener is null!");
			}
			else
			{
				if (!_particleCollisionListener.ParticleSystem.collision.sendCollisionMessages)
				{
					Debug.LogError("Particle system is not sending collision messages!");
				}
			}
		}
	}
}