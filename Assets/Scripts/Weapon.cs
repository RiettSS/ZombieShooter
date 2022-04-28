﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public float MaxAmmo { get; protected set; }
    public float CurrentAmmo { get; protected set; }

    public Damage Damage;

    public virtual void Reload() { }

    public virtual void Shoot() { }
    
}
