using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.Gameplay.Characters.Player
{
	public class PlayerModel
	{
		public HealthComponent HealthComponent { get; }

		public PlayerModel()
		{
			HealthComponent = new HealthComponent(100, 100);
		}
	}
}