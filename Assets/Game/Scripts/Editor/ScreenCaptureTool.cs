#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Editor
{
	public static class ScreenCaptureTool
	{
		[MenuItem("Tools/MakeScreenshot")]
		public static void MakeScreenshot()
		{
			ScreenCapture.CaptureScreenshot("Assets/screenshot.png");
		}
	}
}
#endif
