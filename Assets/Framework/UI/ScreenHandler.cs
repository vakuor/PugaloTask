using System;
using System.Collections.Generic;
using Framework.UI;
using Framework.Utils;

namespace Game.Scripts.UI.Screens
{
	public class ScreenHandler
	{
		private readonly IPrefabCreator _prefabCreator;
		private readonly MainUiCanvas _mainUiCanvas;
		private readonly Dictionary<Type, BaseScreen> _screenLinks;

		public ScreenHandler(IPrefabCreator prefabCreator)
		{
			_prefabCreator = prefabCreator;
			_mainUiCanvas = _prefabCreator.Instantiate<MainUiCanvas>();
			_screenLinks = new Dictionary<Type, BaseScreen>();
		}

		public T GetScreen<T>() where T : BaseScreen
		{
			Type type = typeof(T);
			if (_screenLinks.ContainsKey(type))
			{
				return (T)_screenLinks[type];
			}
			T res = _prefabCreator.InstantiateAt<T>(_mainUiCanvas.transform);
			_screenLinks.Add(type, res);
			return res;
		}
	}
}