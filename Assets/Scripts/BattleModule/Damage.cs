using System;
using UnityEngine;

namespace ZombieShooter.BattleModule
{
    public readonly struct Damage
    {
        public readonly float Amount;
        
        public Damage(float amount)
        {
            if (amount < Mathf.Epsilon)
                throw new ArgumentException();

            Amount = amount;
        }
    }
}