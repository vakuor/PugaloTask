using Framework.Utils;
using Game.Scripts;
using Game.Scripts.UI.Screens;
using UnityEngine;
using Zenject;
using Component = UnityEngine.Component;

//[CreateAssetMenu(fileName = "ScreensInstallerBase", menuName = "Installers/ScreensInstallerBase")]
namespace Framework.UI
{
	public abstract class ScreensInstallerBase : ScriptableObjectInstaller<ScreensInstallerBase>
	{
		[SerializeField]
		private MainUiCanvas _mainUiCanvasPrefab;
		[SerializeField]
		private LoadingScreen _loadingScreenPrefab;

		public override void InstallBindings()
		{
			Container.Bind<ScreenHandler>().AsSingle();
			Container.Bind<IPrefabCreator>().To<PrefabCreator>().AsSingle();
			Container.Bind<Component>().FromInstance(_mainUiCanvasPrefab).WhenInjectedInto<IPrefabCreator>();
			Container.Bind<Component>().FromInstance(_loadingScreenPrefab).WhenInjectedInto<IPrefabCreator>();
			InstallScreens();
		}

		protected abstract void InstallScreens();
	}
}