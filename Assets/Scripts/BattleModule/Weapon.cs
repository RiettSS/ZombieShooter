using UnityEngine;

namespace ZombieShooter.BattleModule
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        public float MaxAmmo { get; protected set; }
        public float CurrentAmmo { get; protected set; }

        public Damage Damage;

        public virtual void Reload() { }

        public virtual void Shoot() { }
    
    }
}
