using System;
using Game.Scripts.Gameplay.Abstractions;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.CharacterBase
{
	public abstract class CharView : MonoBehaviour, IDyingEntity, IDamageableEntity
    {
        [SerializeField]
        public Transform _transform;
        [SerializeField]
        protected CharacterController _characterController;

        public event EventHandler<DamageArgs> OnDamaged;
        public event EventHandler<bool> OnDead;
        
        public void RotateAround(float value) => _transform.Rotate(Vector3.up * value, Space.Self);

        public virtual void ExecuteMovement(Vector3 motion) => _characterController.Move(motion*Time.deltaTime);

        public virtual void TakeDamage(object sender, DamageArgs damageAmount) => OnDamaged?.Invoke(sender, damageAmount);

        public virtual void SetDead(bool dead)
        {
            _characterController.enabled = !dead;
            OnDead?.Invoke(this, dead);
        }

        private void Reset()
        {
            if (_characterController == null)
            {
                _characterController = GetComponent<CharacterController>();
            }
            if (_transform == null)
            {
                _transform = transform;
            }
        }
    }
}