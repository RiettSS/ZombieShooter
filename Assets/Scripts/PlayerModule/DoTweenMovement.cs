using DG.Tweening;
using UnityEngine;

namespace ZombieShooter
{
    public class DoTweenMovement : Movement
    {
        private readonly Transform _transform;

        public DoTweenMovement(Transform transform)
        {
            _transform = transform;
        }

        public override void Move(Vector2 direction)
        {
            _transform.DOMove(_transform.position + (Vector3)direction, 1.0f / _currentSpeed * Time.fixedDeltaTime * 6000, false);
        }
    }
}
