using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.CharacterBase
{
	public abstract class CharController : MonoBehaviour
	{
		[SerializeField]
		private float _moveSpeed = 1f;
		
		protected abstract CharView View { get; }

		protected void Move(Vector3 direction)
		{
			direction.Normalize();
			direction *= _moveSpeed;
			View.ExecuteMovement(direction);
		}
	}
}