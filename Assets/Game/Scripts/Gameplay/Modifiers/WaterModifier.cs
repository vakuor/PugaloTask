namespace Game.Scripts.Gameplay.Modifiers
{
	public class WaterModifier : IDamageModifier
	{
		private readonly int _amount = 10;
		public int Amount => _amount;
	}
}