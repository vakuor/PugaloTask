using UnityEngine;

namespace Framework.Utils
{
	public interface IPrefabCreator
	{
		T Instantiate<T>() where T : Component;
		T InstantiateAt<T>(Transform parent) where T : Component;
	}
}