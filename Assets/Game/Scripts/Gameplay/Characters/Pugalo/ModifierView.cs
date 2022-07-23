using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.Pugalo
{
	public class ModifierView : MonoBehaviour
	{
		[SerializeField]
		private MeshRenderer[] _renderers;

		private bool _onFire;
		public bool OnFire
		{
			get => _onFire;
			set
			{
				SetColor(value ? Color.red : Color.white);
				_onFire = value; 
			}
		}

		private int _onWater;
		public int OnWater
		{
			get => _onWater;
			set
			{
				SetColor(value>0 ? Color.blue : Color.white);
				_onWater = value;
			}
		}

		private void SetColor(Color color)
		{
			foreach (MeshRenderer meshRenderer in _renderers)
			{
				meshRenderer.material.color = color;
			}
		}
	}
}