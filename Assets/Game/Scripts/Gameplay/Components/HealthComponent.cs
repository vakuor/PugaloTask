using System;
using Game.Scripts.Gameplay.Abstractions;
using Game.Scripts.Gameplay.Modifiers;
using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
	public class HealthComponent
	{
		public event EventHandler<int> OnHealthChanged;
		public event EventHandler<bool> OnDead;
		public event EventHandler<bool> OnFireEvent;
		public event EventHandler<int> OnWaterEvent;

		private bool _onFire;

		public bool OnFire
		{
			get => _onFire;
			set
			{
				_onFire = value;
				OnFireEvent?.Invoke(this, _onFire);
			}
		}

		private int _onWater;

		public int OnWater
		{
			get => _onWater;
			set
			{
				if (value > 100) value = 100;
				_onWater = value;
				OnWaterEvent?.Invoke(this, _onWater);
			}
		}

		public readonly int MaxHealth;
		private int _health;
		public int Health => _health;
		public bool Dead => _dead;

		private bool _dead = false;

		public HealthComponent(int health, int maxHealth)
		{
			_health = health;
			MaxHealth = maxHealth;
		}

		public void Heal(int healAmount)
		{
			if (healAmount <= 0)
			{
				throw new Exception("Heal amount can't be less or equal 0");
			}
			if (_dead)
			{
				throw new Exception("Can't heal dead player!");
			}
			if (_health + healAmount > MaxHealth)
			{
				_health = MaxHealth;
			}
			else
			{
				_health += healAmount;
			}
			OnHealthChanged?.Invoke(this, _health);
		}

		public void Damage(DamageArgs damageAmount)
		{
			if (_dead)
			{
				return;
			}
			if (damageAmount.Damage < 0)
			{
				throw new Exception("Damage amount can't be less 0");
			}
			int finalDamage = damageAmount.Damage;
			if (damageAmount.Modifier != null)
			{
				switch (damageAmount.Modifier)
				{
					case BulletModifier _:
						if (OnFire)
						{
							finalDamage += 10;
						}
						if (OnWater > 0)
						{
							finalDamage -= 10;
						}
						break;
					case FireModifier _:
						Burn(damageAmount.Modifier.Amount);
						break;
					case WaterModifier _:
						Choke(damageAmount.Modifier.Amount);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
			Debug.Log("Damage: " + finalDamage);
			_health -= finalDamage;
			if (_health <= 0)
			{
				_health = 0;
				OnHealthChanged?.Invoke(this, _health);
				Kill();
			}
			else
			{
				OnHealthChanged?.Invoke(this, _health);
			}
		}

		public void Revive()
		{
			if (!_dead)
			{
				throw new Exception("Can't revive alive player!");
			}
			_dead = false;
			OnDead?.Invoke(this, _dead);
		}

		public void Kill()
		{
			if (_dead)
			{
				throw new Exception("Can't kill dead player!");
			}
			_health = 0;
			OnHealthChanged?.Invoke(this, _health);
			_dead = true;
			OnDead?.Invoke(this, _dead);
		}

		private void Burn(int amount)
		{
			if (OnWater > 0)
			{
				if (OnWater - amount < 0)
				{
					OnWater = 0;
					OnFire = true;
				}
				else
				{
					OnWater -= amount;
				}
			}
			else
			{
				OnFire = true;
			}
		}

		private void Choke(int amount)
		{
			if (OnFire)
			{
				OnFire = false;
			}
			Debug.Log(amount);
			OnWater += amount;
			Debug.Log(OnWater);
		}
	}
}