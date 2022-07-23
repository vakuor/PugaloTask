using Game.Scripts.Gameplay.Abstractions;
using Game.Scripts.Gameplay.Items.Base;
using Game.Scripts.Gameplay.Modifiers;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Stones
{
	public class WaterBall : Item
	{
		public override bool CanBeUsed() => false;
		public override bool TryUse() => false;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.TryGetComponent(out IDamageableEntity damageableEntity))
			{
				damageableEntity.TakeDamage(this, new DamageArgs{Modifier = new WaterModifier()});
			}
			Destroy(gameObject);
		}
	}
}