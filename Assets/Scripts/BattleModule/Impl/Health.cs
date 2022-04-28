using System;
using UnityEngine;

namespace ZombieShooter.BattleModule.Impl
{
    [Serializable]
    public struct Health
    {
        private const float Min = 0; 
        public readonly float Amount; 
        public readonly float Max;
    
        public bool IsEmpty { get; }
        public Health(float hp, float max)
        {
            if (hp - max > 0)
                throw new ArgumentException();
            
            if (hp < Min)
                throw new ArgumentException();

            if (max < 0)
                throw new ArgumentException();

            Amount = hp;
            Max = max;
            IsEmpty = hp < Mathf.Epsilon;
        }

        public Health TakeDamage(Damage damage)
        {
            return Amount - damage.Amount < Mathf.Epsilon ? new Health(0, Max) : new Health(Amount - damage.Amount, Max);
        }

        public Health AddHealth(Health health)
        {
            return Amount + health.Amount >= Max ? new Health(Max, Max) : new Health(Amount + health.Amount, Max);
        }
    }
}