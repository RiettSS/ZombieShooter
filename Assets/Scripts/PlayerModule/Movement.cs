using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public abstract class Movement : IMovement
    {
        protected float _walkingSpeed = 350;
        protected float _runningSpeed = 420;
        protected float _stamina = 100;
        protected float _stamingReducingSpeed = 0.2f;
        protected float _staminaRegenerationSpeed = 0.2f;
        protected float _currentSpeed;

        public Movement()
        {
            _currentSpeed = _walkingSpeed;
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

        public abstract void Move(Vector2 direction);
        
    }
}
