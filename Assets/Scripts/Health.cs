using System;
using UnityEngine;

[Serializable]
public struct Health
{
    private const float Min = 0; 
    public readonly float Current; 
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

        Current = hp;
        Max = max;
        IsEmpty = hp < Mathf.Epsilon;
    }

    public Health TakeDamage(Damage damage)
    {
        return Current - damage.Amount < Mathf.Epsilon ? new Health(0, Max) : new Health(Current - damage.Amount, Max);
    }

    public Health AddHealth(float hp)
    {
        return Current + hp >= Max ? new Health(Max, Max) : new Health(Current + hp, Max);
    }
}