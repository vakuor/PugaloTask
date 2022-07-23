namespace Game.Scripts.Gameplay.Modifiers
{
	public class FireModifier : IDamageModifier
	{
		private readonly int _amount = 1;
		public int Amount => _amount;
	}
}