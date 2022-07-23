using Framework.Gameplay;
using Framework.UI;
using Game.Scripts.UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Gameplay.Screens
{
	public class MainMenuScreen : BaseScreen
	{
		[SerializeField]
		private Button _startBtn;
		private SceneHandler _sceneHandler;
		private ScreenHandler _screenHandler;
		
		[Inject]
		public void Construct(SceneHandler sceneHandler, ScreenHandler screenHandler)
		{
			_sceneHandler = sceneHandler;
			_screenHandler = screenHandler;
			_startBtn.onClick.RemoveAllListeners();
			_startBtn.onClick.AddListener(StartGame);
		}

		private void StartGame()
		{
			_sceneHandler.SceneLoaded += OnDefaultSceneLoaded;
			_sceneHandler.StartScene(DefaultSettings.DefaultScene);
		}

		private void OnDefaultSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			_sceneHandler.SceneLoaded -= OnDefaultSceneLoaded;
			Hide();
		}
	}
}