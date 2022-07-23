using Game.Scripts.Gameplay.Items.Base;

namespace Game.Scripts.Gameplay.Items.Box
{
	public class BoxItem : Item
	{
		public override bool TryUse()
		{
			ThrowPhysically(_transform.position, _transform.forward, 400f);
			return true;
		}
	}
}