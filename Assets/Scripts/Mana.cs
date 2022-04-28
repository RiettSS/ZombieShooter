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

    public Mana Reduce(Mana mana)
    {
        return Amount - mana.Amount < Mathf.Epsilon ? new Mana(0, Max) : new Mana(Amount - mana.Amount, Max);
    }

    public Mana Add(Mana mana)
    {
        return Amount + mana.Amount >= Max ? new Mana(Max, Max) : new Mana(Amount + mana.Amount, Max);
    }
}
