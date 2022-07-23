using System;
using Game.Scripts.Gameplay.Abstractions;
using Game.Scripts.Gameplay.Items.Base;
using Game.Scripts.Gameplay.Modifiers;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Pistol
{
	public class WeaponView : WeaponBase
	{
		[SerializeField]
		private Transform _muzzle;
		public event EventHandler OnViewShotMade;

		private void Awake() => WeaponModel = new WeaponModel();
		private void OnEnable() => WeaponModel.OnShotMade += MakeShot;
		private void OnDisable() => WeaponModel.OnShotMade -= MakeShot;

		public void MakeShot(object sender, WeaponShotArgs weaponShotArgs)
		{
			if (Physics.Raycast(_muzzle.position, _muzzle.forward, out RaycastHit hit, weaponShotArgs.Range,
						weaponShotArgs.CollisionMask))
			{
				bool targetDamageable = hit.collider.TryGetComponent(out IDamageableEntity target);
				if (targetDamageable)
				{
					target.TakeDamage(this, new DamageArgs { Damage = weaponShotArgs.Damage, Modifier = new BulletModifier()});
				}
#if UNITY_EDITOR
				UnityEngine.Debug.DrawLine(_muzzle.position, hit.point, Color.red, 3f);
#endif
			}
#if UNITY_EDITOR
			else
			{
				UnityEngine.Debug.DrawLine(_muzzle.position, _muzzle.position + _muzzle.forward * weaponShotArgs.Range,
						Color.red, 3f);
			}
#endif
			OnViewShotMade?.Invoke(this, EventArgs.Empty);
		}
	}
}