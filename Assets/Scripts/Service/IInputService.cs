using System;
using UnityEngine;

namespace ZombieShooter.Service
{
    public interface IInputService
    {
        public event Action<Vector2> MovementDirectionUpdated;
        public event Action BoostPressed;
        public event Action BoostUnpressed;
        public event Action Fire;
    }
}
