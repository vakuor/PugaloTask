using Game.Scripts.Gameplay.Characters.CharacterBase;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.Pugalo
{
	public class PugaloView : CharView
	{
		[SerializeField]
		private ModifierView _modifierView;
		public ModifierView ModifierView => _modifierView;
		[SerializeField]
		private DamageableListener[] _damageableColliderListeners;

		private void Start() => _characterController.enabled = false;
		
		public override void SetDead(bool dead)
		{
			base.SetDead(dead);
			Debug.Log("dead changed: " + dead);
			_modifierView.gameObject.SetActive(!dead);
		}

		private void OnEnable()
		{
			foreach (var listener in _damageableColliderListeners)
			{
				listener.OnDamaged += TakeDamage;
			}
		}
		
		private void OnDisable()
		{
			foreach (var listener in _damageableColliderListeners)
			{
				listener.OnDamaged += TakeDamage;
			}
		}
	}
}