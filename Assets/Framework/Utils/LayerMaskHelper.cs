using System;
using UnityEngine;

namespace Framework.Utils
{
	public static class LayerMaskHelper
	{
		public const int Everything = 1;
		public const int Nothing = 0;
		public static readonly int ExceptPlayerMask = InvertMask(GetLayersMask(LayerMask.NameToLayer("LocalPlayer")));
		public static readonly int ExceptItemMask = InvertMask(GetLayersMask(LayerMask.NameToLayer("Item")));
		
		public static int GetLayersMask(params int[] layers)
		{
			int currentMask = Nothing;
			for (int i = 0; i < layers.Length; i++)
			{
				int mask = 1 << layers[i];
				currentMask |= mask; // same as: currentMask = CombineMasks(currentMask, mask);
			}
			return currentMask;
		}

		public static string MaskToString(int mask) => Convert.ToString(mask, 2);
		public static int InvertMask(int mask) => ~mask;
		public static int CombineMasks(int firstMask, int secondMask) => firstMask | secondMask;
	}
}