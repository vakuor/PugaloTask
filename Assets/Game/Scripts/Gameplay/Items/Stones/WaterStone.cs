using System;
using UnityEngine;

namespace Game.Scripts.Gameplay.Items.Stones
{
	public class WaterStone : StoneItemBase
	{
		[SerializeField]
		private WaterBall _waterBallPrefab;
		protected override void Use()
		{
			base.Use();
			ThrowWaterBall(_transform.position, _transform.forward, 450f);
		}

		private void ThrowWaterBall(Vector3 from, Vector3 direction, float force)
		{
			bool created = Instantiate(_waterBallPrefab, from, Quaternion.identity).TryGetComponent(out WaterBall ball);
			if (!created)
			{
				Debug.LogException(new Exception("Water ball creation fail!"));
			}
			ball.ThrowPhysically(from, direction, force);
		}
	}
}