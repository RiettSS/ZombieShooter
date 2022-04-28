using System;
using UnityEngine;

namespace ZombieShooter.Service.Impl
{
    public class InputService : MonoBehaviour, IInputService
    {
        public event Action<Vector2> MovementDirectionUpdated;
        
        public event Action BoostPressed;
        public event Action BoostUnpressed;
        public event Action Fire;
        public event Action WeaponOnePressed;
        public event Action WeaponTwoPressed;
        public event Action WeaponThreePressed;

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            
            MovementDirectionUpdated?.Invoke(new Vector2(x, y));
            
            if (Input.GetKey(KeyCode.LeftShift))
                BoostPressed?.Invoke();
            else
                BoostUnpressed?.Invoke();
        
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Fire?.Invoke();

            if (Input.GetKey(KeyCode.Mouse1))
                Fire?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                WeaponOnePressed?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                WeaponTwoPressed?.Invoke();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                WeaponThreePressed?.Invoke();
        }
    }
}
