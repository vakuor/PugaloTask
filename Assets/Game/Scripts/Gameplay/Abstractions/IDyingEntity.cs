using System;

namespace Game.Scripts.Gameplay.Abstractions
{
	public interface IDyingEntity
	{
		public event EventHandler<bool> OnDead;
		void SetDead(bool dead);
	}
}