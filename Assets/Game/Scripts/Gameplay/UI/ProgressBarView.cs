using UnityEngine;

namespace Game.Scripts.Gameplay.UI
{
	public class ProgressBarView : MonoBehaviour
	{
		[SerializeField]
		private Transform _bar;

		public void SetValue(int value, int maxValue)
		{
			_bar.transform.localScale = new Vector3((float)value/maxValue, 1f, 1f);
		}
	}
}