using System.Threading.Tasks;
using Game.Scripts.Gameplay.Screens;
using Game.Scripts.UI.Screens;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Startup : IInitializable, ITickable
	{
		private ScreenHandler _screenHandler;
	
		[Inject]
		public void Construct(ScreenHandler screenHandler)
		{
			_screenHandler = screenHandler;
		}

		public async void Initialize()
		{
			UnityEngine.Debug.Log("Client initialization");
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		
			UnityEngine.Debug.Log("Client started");
			LoadingScreen loadingScreen = _screenHandler.GetScreen<LoadingScreen>();
			loadingScreen.Init(Task.Delay(1), () => _screenHandler.GetScreen<MainMenuScreen>());
			await loadingScreen.Load();
		}

		public void Tick()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
		}
	}
}