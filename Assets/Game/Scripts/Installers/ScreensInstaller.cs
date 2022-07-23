using System.Collections.Generic;
using Framework.UI;
using Framework.Utils;
using UnityEngine;

namespace Game.Scripts.Installers
{
	[CreateAssetMenu(fileName = "ScreensInstaller", menuName = "Installers/ScreensInstaller")]
	public class ScreensInstaller : ScreensInstallerBase
	{
		[SerializeField]
		private List<BaseScreen> _screenPrefabs;
		protected override void InstallScreens()
		{
			foreach (BaseScreen screen in _screenPrefabs)
			{
				Container.Bind<Component>().FromInstance(screen).WhenInjectedInto<IPrefabCreator>();
			}
		}
	}
}