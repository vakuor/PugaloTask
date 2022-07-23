using Game.Scripts.Gameplay.Items.Base;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Stones
{
	public abstract class StoneItemBase : Item
	{
		[SerializeField]
		private float _useDelay = 0.1f;
		private float _lastUseTime;
		
		public override bool CanBeUsed() => Time.time >= _lastUseTime + _useDelay;
		protected virtual void Use() => _lastUseTime = Time.time;

		public override bool TryUse()
		{
			bool canBeUsed = CanBeUsed();
			if (canBeUsed)
			{
				Use();
			}
			return canBeUsed;
		}
	}
}