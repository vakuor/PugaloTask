using Framework.Utils;
using Game.Scripts.Gameplay.Characters.CharacterBase;
using Game.Scripts.Gameplay.Items.Base;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.Player
{
	public class PlayerView : CharView
	{
		[SerializeField]
		private Camera _camera;
		[SerializeField]
		private Transform _leftHand;
		[SerializeField]
		private Transform _rightHand;
		[SerializeField]
		private Transform _dropPivot;

		private float _cameraVerticalAngle;

		public void InputX(float value) => RotateAround(value);

		public void InputY(float value)
		{
			_cameraVerticalAngle -= value;
			_cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);
			_camera.transform.localEulerAngles = Vector3.right * _cameraVerticalAngle;
		}

		public bool TryFindItemInScope(out Item item)
		{
			if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, 5f,
						LayerMaskHelper.ExceptPlayerMask))
			{
				bool targetExists = hit.collider.TryGetComponent(out Item target);
				if (targetExists)
				{
					item = target;
					return true;
				}
			}
			item = null;
			return false;
		}

		public void PlaceItemInHand(Item item, bool leftHand)
		{
			Transform hand = leftHand ? _leftHand : _rightHand;
			item.Attach(hand);
		}

		public void DropItem(Item item)
		{
			item.Detach();
			item.Transform.position = _dropPivot.position;
		}

		public override void SetDead(bool dead)
		{
			base.SetDead(dead);
			UnityEngine.Debug.Log("Player dead");
		}
	}
}