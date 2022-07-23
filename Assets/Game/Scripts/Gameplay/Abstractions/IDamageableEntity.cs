using System;
using Game.Scripts.Gameplay.Modifiers;

namespace Game.Scripts.Gameplay.Abstractions
{
	public interface IDamageableEntity
	{
		public event EventHandler<DamageArgs> OnDamaged;
		public void TakeDamage(object sender, DamageArgs damageInfo);
	}

	public class DamageArgs : EventArgs
	{
		public IDamageModifier Modifier { get; set; }
		public int Damage { get; set; }
	}
}