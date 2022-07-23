using System.Collections;
using Game.Scripts.Gameplay.Abstractions;
using Game.Scripts.Gameplay.Characters.CharacterBase;
using Game.Scripts.Gameplay.UI;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.Pugalo
{
	public class PugaloController : CharController
	{
		[SerializeField]
		private PugaloView _pugaloView;
		[SerializeField]
		private ProgressBarView _healthBarView;
		[SerializeField]
		private ProgressBarView _waterBarView;
		protected override CharView View => _pugaloView;
		public PugaloModel Model { get; set; }

		private Coroutine _fireRoutine;
		
		private void HealthSystem_OnHealthChanged(object sender, int e) => _healthBarView.SetValue(e, Model.HealthComponent.MaxHealth);
		private void HealthSystem_OnDead(object sender, bool dead) => _pugaloView.SetDead(dead);

		private void Awake() => Model = new PugaloModel();

		private void OnEnable()
		{
			_pugaloView.OnDamaged += HandleDamage;
			Model.HealthComponent.OnFireEvent += OnFireEvent;
			Model.HealthComponent.OnWaterEvent += OnWaterEvent;
			OnWaterEvent(this, Model.HealthComponent.OnWater);
			Model.HealthComponent.OnHealthChanged += HealthSystem_OnHealthChanged;
			HealthSystem_OnHealthChanged(this, Model.HealthComponent.Health);
			Model.HealthComponent.OnDead += HealthSystem_OnDead;
		}

		private void OnDisable()
		{
			_pugaloView.OnDamaged -= HandleDamage;
			Model.HealthComponent.OnFireEvent -= OnFireEvent;
			Model.HealthComponent.OnWaterEvent -= OnWaterEvent;
			Model.HealthComponent.OnHealthChanged -= HealthSystem_OnHealthChanged;
			Model.HealthComponent.OnDead -= HealthSystem_OnDead;
		}

		private void OnFireEvent(object sender, bool e)
		{
			_pugaloView.ModifierView.OnFire = e;
			if (_fireRoutine!=null)
			{
				StopCoroutine(_fireRoutine);
			}
			if (e)
			{
				_fireRoutine = StartCoroutine(FireRoutine(10f, 1f));
			}
		}

		private void OnWaterEvent(object sender, int e)
		{
			_pugaloView.ModifierView.OnWater = e;
			_waterBarView.SetValue(e, 100);
		}

		private void HandleDamage(object sender, DamageArgs damageAmount) => Model.HealthComponent.Damage(damageAmount);

		IEnumerator FireRoutine(float length, float damageEvery)
		{
			float startTime = Time.time;
			while (Time.time < startTime + length)
			{
				yield return new WaitForSeconds(damageEvery);
				if (Model.HealthComponent.Dead)
				{
					break;
				}
				HandleDamage(this, new DamageArgs{Damage = 5});
			}
			Model.HealthComponent.OnFire = false;
		}
	}
}