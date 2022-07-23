using System;
using Game.Scripts.Gameplay.Modifiers;

namespace Game.Scripts.Gameplay.Items.Pistol
{
	public class WeaponShotArgs : EventArgs
	{
		public int Damage { get; }
		public float Range { get; }
		public float HitForce { get; }
		public int CollisionMask { get; }

		public WeaponShotArgs(int damage, float range, float hitForce, int collisionMask)
		{
			Damage = damage;
			Range = range;
			HitForce = hitForce;
			CollisionMask = collisionMask;
		}
	}
}