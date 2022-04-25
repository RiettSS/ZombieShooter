using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public float MaxAmmo { get; protected set; }
    public float CurrentAmmo { get; protected set; }

    public float Damage { get; set; }

    private float _defaultDamage = 5;

    private void Awake()
    {
        Damage = _defaultDamage;
    }

    public virtual void Reload() { }

    public virtual void Shoot() { }
    
}
