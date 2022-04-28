using UnityEngine;

namespace ZombieShooter.EnemyModule
{
    public class Movement
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly float _maximumDistance;
        
        private Vector3 Position => _rigidbody.position;
        private Transform Transform => _rigidbody.transform;
        
        public Movement(Rigidbody2D rigidbody, float maximumDistance)
        {
            _rigidbody = rigidbody;
            _maximumDistance = maximumDistance;
        }

        public bool TryMoveTo(Transform target)
        {
            if (target == null || !IsTargetInRange(target))
            {
                return false;
            }

            var targetPosition = target.transform.position;
            var direction = (targetPosition - Position).normalized;

            LookInDirection(direction);
            MoveInDirection(direction);

            return true;
        }

        private void LookInDirection(Vector3 direction)
        {
            Transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg));
        }

        private void MoveInDirection(Vector3 direction)
        {
            if (_rigidbody.velocity.magnitude < 1)
            {
                _rigidbody.velocity = 150 * direction * Time.fixedDeltaTime;
            }
        }
        
        private bool IsTargetInRange(Transform target)
        {
            return Vector3.Distance(Position, target.position) < _maximumDistance;
        }
    }
}