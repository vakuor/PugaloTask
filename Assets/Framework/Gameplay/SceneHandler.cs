using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Framework.Gameplay
{
	[UsedImplicitly]
	public class SceneHandler
	{
		public event UnityAction<Scene, LoadSceneMode> SceneLoaded;
		public event UnityAction<Scene> SceneUnloaded;
		private string _loadedScene;
		private string _requestedScene;

		public void StartScene(string sceneName)
		{
			if (!string.IsNullOrWhiteSpace(_loadedScene))
			{
				StopScene();
			}
			_requestedScene = sceneName;
			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
			if (scene.name != _requestedScene)
			{
				return;
			}
			SceneManager.sceneLoaded -= OnSceneLoaded;
			_loadedScene = _requestedScene;
			SceneLoaded?.Invoke(scene, loadSceneMode);
		}

		public void StopScene()
		{
			if (string.IsNullOrWhiteSpace(_loadedScene))
			{
				Debug.LogWarning("You are trying to stop scene but there is no loaded scene!");
				return;
			}
			_requestedScene = null;
			SceneManager.sceneUnloaded += OnSceneUnloaded;
			SceneManager.UnloadScene(_loadedScene);
		}
		
		private void OnSceneUnloaded(Scene scene)
		{
			if (scene.name != _requestedScene)
			{
				return;
			}
			SceneManager.sceneUnloaded -= OnSceneUnloaded;
			_loadedScene = null;
			SceneUnloaded?.Invoke(scene);
		}
	}
}