using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public interface IMovement
    {
        void Move(Vector2 direction);
        void Boost();
        void BoostRegeneration();
    }
}
