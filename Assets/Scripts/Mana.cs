using System;
using UnityEngine;

public struct Mana
{
    private const float Min = 0;
    public readonly float Amount;
    public readonly float Max;

    public bool IsEmpty { get; }
    public Mana(float hp, float max)
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

    public Mana TakeDamage(Damage damage)
    {
        return Amount - damage.Amount < Mathf.Epsilon ? new Mana(0, Max) : new Mana(Amount - damage.Amount, Max);
    }

    public Mana AddHealth(Health health)
    {
        return Amount + health.Amount >= Max ? new Mana(Max, Max) : new Mana(Amount + health.Amount, Max);
    }
}
