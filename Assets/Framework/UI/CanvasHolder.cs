using System;
using UnityEngine;

namespace Framework.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class CanvasHolder : MonoBehaviour
	{
		[SerializeField]
		private Canvas _canvas;

		private void OnValidate()
		{
			if (!TryGetComponent(out _canvas))
			{
				throw new Exception();
			}
		}

		public void Disable()
		{
			_canvas.enabled = false;
		}

		public void Enable()
		{
			_canvas.enabled = true;
		}
	}
}