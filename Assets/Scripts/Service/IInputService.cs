using System;
using UnityEngine;

namespace ZombieShooter.Service
{
    public interface IInputService
    {
        event Action<Vector2> MovementDirectionUpdated;
        event Action BoostPressed;
        event Action BoostUnpressed;
        event Action Fire;
        event Action WeaponOnePressed;
        event Action WeaponTwoPressed;
        event Action WeaponThreePressed;

    }
}
