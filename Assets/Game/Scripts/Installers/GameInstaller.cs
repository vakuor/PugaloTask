using Framework.Gameplay;
using Zenject;

namespace Game.Scripts.Installers
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<SceneHandler>().AsSingle();
			Container.BindInterfacesAndSelfTo<Startup>().AsTransient();
		}
	}
}