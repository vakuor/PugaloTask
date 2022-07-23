using Game.Scripts.Gameplay.Abstractions;
using Game.Scripts.Gameplay.Characters.CharacterBase;
using Game.Scripts.Gameplay.Items.Base;
using UnityEngine;

namespace Game.Scripts.Gameplay.Characters.Player
{
	public class PlayerController : CharController
	{
		[SerializeField]
		private float _mouseSensitivity = 1f;
		[SerializeField]
		private PlayerView _playerView;
		protected override CharView View => _playerView;
		private PlayerModel Model { get; set; }

		private Item _itemLeft;
		private Item _itemRight;
		private Vector3 _moveToExecute;

		private void Awake() => Model = new PlayerModel();

		private void OnEnable()
		{
			_playerView.OnDamaged += HandleDamage;
			Model.HealthComponent.OnDead += HealthSystem_OnDead;
		}

		private void OnDisable()
		{
			_playerView.OnDamaged -= HandleDamage;
			Model.HealthComponent.OnDead -= HealthSystem_OnDead;
		}

		private void HealthSystem_OnDead(object sender, bool dead)
		{
			_playerView.SetDead(dead);
		}

		private void HandleDamage(object sender, DamageArgs damage) => Model.HealthComponent.Damage(
				new DamageArgs
				{
						Damage = damage.Damage
				});

		private void Update()
		{
			if (Model.HealthComponent.Dead)
			{
				return;
			}
			HandleLook();
			HandleShoot();
			HandleMovement();
		}

		private void HandleShoot()
		{
			if (Input.GetKey(KeyCode.Mouse0))
			{
				Use(_itemLeft);
			}
			if (Input.GetKey(KeyCode.Mouse1))
			{
				Use(_itemRight);
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				TryTakeItem(true);
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				TryTakeItem(false);
			}
		}

		private void Use(Item item)
		{
			if (item == null || !item.CanBeUsed())
			{
				return;
			}
			item.TryUse();
		}

		private void TryTakeItem(bool leftHand)
		{
			DropItem(leftHand);
			bool itemFound = _playerView.TryFindItemInScope(out Item item);
			if (!itemFound)
			{
				return;
			}
			if (leftHand)
			{
				_itemLeft = item;
			}
			else
			{
				_itemRight = item;
			}
			_playerView.PlaceItemInHand(item, leftHand);
			item.OnDetached += OnHandItemDetached;
		}

		private void OnHandItemDetached(object sender, AttachEventArgs e)
		{
			e.Item.OnDetached -= OnHandItemDetached;
			if (e.Item == _itemLeft)
			{
				_itemLeft = null;
			}
			else if (e.Item == _itemRight)
			{
				_itemRight = null;
			}
		}

		private void DropItem(bool leftHand)
		{
			Item item = leftHand ? _itemLeft : _itemRight;
			DropItem(item);
			if (leftHand)
			{
				_itemLeft = null;
			}
			else
			{
				_itemRight = null;
			}
		}

		private void DropItem(Item item)
		{
			if (item == null)
			{
				return;
			}
			_playerView.DropItem(item);
		}

		private void HandleMovement()
		{
			_moveToExecute = Vector3.zero;

			Transform viewTransform = _playerView._transform;
			Vector3 transformRight = viewTransform.right;
			Vector3 transformForward = viewTransform.forward;
			Vector2 direction = Vector2.zero;

			if (Input.GetKey(KeyCode.W))
			{
				direction.y++;
			}
			if (Input.GetKey(KeyCode.S))
			{
				direction.y--;
			}

			if (Input.GetKey(KeyCode.D))
			{
				direction.x++;
			}
			if (Input.GetKey(KeyCode.A))
			{
				direction.x--;
			}

			_moveToExecute += transformForward * direction.y;
			_moveToExecute += transformRight * direction.x;

			Move(_moveToExecute.normalized);
		}

		private void HandleLook()
		{
			float lookX = Input.GetAxisRaw("Mouse X");
			float lookY = Input.GetAxisRaw("Mouse Y");
			_playerView.InputX(lookX * _mouseSensitivity);
			_playerView.InputY(lookY * _mouseSensitivity);
		}
	}
}