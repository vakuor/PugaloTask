using Game.Scripts.Gameplay.Components;

namespace Game.Scripts.Gameplay.Characters.Pugalo
{
	public class PugaloModel
	{
		public HealthComponent HealthComponent { get; }

		public PugaloModel()
		{
			HealthComponent = new HealthComponent(1000, 1000);
		}
	}
}