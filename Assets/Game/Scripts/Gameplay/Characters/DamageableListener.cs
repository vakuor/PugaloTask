using System;
using Game.Scripts.Gameplay.Abstractions;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters
{
	public class DamageableListener : MonoBehaviour, IDamageableEntity
	{
		public event EventHandler<DamageArgs> OnDamaged;
		public void TakeDamage(object sender, DamageArgs damage)
		{
			OnDamaged?.Invoke(sender, damage);
		}
	}
}