using Game.Scripts.Gameplay.Characters.Pugalo;
using UnityEngine;

namespace Game.Scripts
{
	public class UIDebug : MonoBehaviour
	{
		[SerializeField]
		private PugaloController _view;
		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				if (_view.Model.HealthComponent.Dead)
				{
					_view.Model.HealthComponent.Revive();
				}
				_view.Model.HealthComponent.Heal(1000);
			}
		}
	}
}
