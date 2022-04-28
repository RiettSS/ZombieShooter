using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.BattleModule;

namespace ZombieShooter
{
    public abstract class WeaponPickUpStrategy
    {
        public abstract void Add(List<Weapon> weaponSlots, Weapon weapon);

        protected bool HasWeapon(List<Weapon> weapons, Weapon weapon)
        {
            foreach (var w in weapons)
            {
                if (w.Type == weapon.Type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
