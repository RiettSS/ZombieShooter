using System;
using UnityEngine;

namespace ZombieShooter.BattleModule
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public float MaxAmmo { get; protected set; }
        public float CurrentAmmo { get; protected set; }

        public Damage Damage;
        [HideInInspector]
        public WeaponType Type;

        public virtual void Reload() { }

        public virtual void Shoot() { }
    }

    public enum WeaponType
    {
        Knife = 0,
        Staff = 1
    }
}
