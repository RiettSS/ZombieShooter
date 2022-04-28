using System;
using UnityEngine;

public interface IDamagable
{
    void ApplyDamage(Damage damage);
}

public struct Damage
{
    public readonly float Amount;
    public Damage(float amount)
    {
        if (amount < Mathf.Epsilon)
            throw new ArgumentException();

        Amount = amount;
    }
}
