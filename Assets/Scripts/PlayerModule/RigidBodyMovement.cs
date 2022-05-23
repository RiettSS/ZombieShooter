using UnityEngine;

namespace ZombieShooter.PlayerModule
{
    public class RigidBodyMovement : Movement
    {
        private readonly Rigidbody2D _rigidbody;
    
        public RigidBodyMovement(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
            _currentSpeed = _walkingSpeed;
        }

        public override void Move(Vector2 direction)
        {
            _rigidbody.velocity = direction * _currentSpeed * Time.fixedDeltaTime;
        }
    }
}
