using UnityEngine;

namespace ZombieShooter.PlayerModule
{
    public class Movement
    {
        private float _walkingSpeed = 220.5f;
        private float _runningSpeed = 280.5f;
        private float _stamina = 100;
        private float _stamingReducingSpeed = 0.2f;
        private float _staminaRegenerationSpeed = 0.2f;
        private float _currentSpeed;

        private readonly Rigidbody2D _rigidbody;
    
        public Movement(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
            _currentSpeed = _walkingSpeed;
        }

        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = direction * _currentSpeed * Time.fixedDeltaTime;
        }

        public void Boost()
        {
            if (_stamina - _stamingReducingSpeed > Mathf.Epsilon)
            {
                _stamina -= _stamingReducingSpeed;
                _currentSpeed = _runningSpeed;
            } 
            else
            {
                _currentSpeed = _walkingSpeed;
            }
        }

        public void BoostRegeneration()
        {
            _currentSpeed = _walkingSpeed;

            if (_stamina < 100)
                _stamina += _staminaRegenerationSpeed;
        }
    }
}
