﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IBonusVisitor
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private PlayerInput _input;

    //public float Health { get; private set; }
    //public float MaxHealth { get; private set; }

    [SerializeField] private Health Health;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _input.Fire += Shoot;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bonus bonus))
        {
            bonus.Apply(this);
        }

    }

    private void Shoot()
    {
        _weapon.Shoot();
    }

    public void AddHealth(float hp)
    {
        Health = Health.AddHealth(hp);
    }

    public void TakeDamage(float hp)
    {
        Health = Health.TakeDamage(hp);
    }

    public void Visit(HealBonus bonus, float hpToHeal)
    {
        AddHealth(hpToHeal);
    }

    public void Visit(DamageMultiplierBonus bonus, float multiplier, float bonusDuration)
    {
        StartCoroutine(DamageMultiplier(multiplier, bonusDuration));
    }

    private IEnumerator DamageMultiplier(float multiplier, float bonusDuration)
    {
        _weapon.Damage *= multiplier;
        yield return new WaitForSeconds(bonusDuration);
        _weapon.Damage /= multiplier;
    }
}


[Serializable]
public struct Health
{
    private const float Min = 0;
    [SerializeField] public readonly float Current;
    [SerializeField] private readonly float Max;

    //fix issue with Mathf.Epsilon
    public Health(float hp, float max)
    {
        if (hp - max > 0)
            throw new ArgumentException();
            
        if (hp < 0)
            throw new ArgumentException();

        if (max < 0)
            throw new ArgumentException();

        Current = hp;
        Max = max;
    }

    public Health TakeDamage(float hp)
    {
        if (Current - hp < Mathf.Epsilon)
        {
            return new Health(0, Max);
        }
        else
        {
            return new Health(Current - hp, Max);
        }
    }

    public Health AddHealth(float hp)
    {
        if (Current + hp >= Max)
        {
            return new Health(Max, Max);
        }
        else
        {
            return new Health(Current + hp, Max);
        }
    }
}
