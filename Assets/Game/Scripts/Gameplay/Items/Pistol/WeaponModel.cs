using System;
using Framework.Utils;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Pistol
{
	public class WeaponModel
	{
		public event EventHandler<WeaponShotArgs> OnShotMade;
		public event EventHandler<int> OnBulletsAmountChanged;
		
		public int BulletsAmount => _bulletsAmount;
		private int _bulletsAmount = 100;

		private int _damage = 20;
		private float _shotRange = 100;

		private float _lastShotTime;
		private float _shotDelay = .1f;

		public bool TryShoot()
		{
			if (CanShoot())
			{
				InvokeShot();
				return true;
			}
			return false;
		}
		
		public bool CanShoot() => ShotTimeHasPassed();// && _bulletsAmount > 0;

		public void AddAmmo(int amount)
		{
			_bulletsAmount += amount;
			OnBulletsAmountChanged?.Invoke(this, _bulletsAmount);
		}

		private void InvokeShot()
		{
			_lastShotTime = Time.time;
			_bulletsAmount--;
			OnBulletsAmountChanged?.Invoke(this, _bulletsAmount);
			OnShotMade?.Invoke(this, new WeaponShotArgs(_damage, _shotRange, 0, LayerMaskHelper.ExceptPlayerMask | LayerMaskHelper.ExceptItemMask));
		}

		private bool ShotTimeHasPassed() => Time.time >= _lastShotTime + _shotDelay;
	}
}