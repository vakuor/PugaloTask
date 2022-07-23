using Game.Scripts.Gameplay.Items.Pistol;

namespace Game.Scripts.Gameplay.Items.Base
{
	public abstract class WeaponBase : Item
	{
		public WeaponModel WeaponModel { get; protected set; }
		
		public override bool CanBeUsed() => WeaponModel.CanShoot();
		public override bool TryUse() => WeaponModel.TryShoot();
	}
}