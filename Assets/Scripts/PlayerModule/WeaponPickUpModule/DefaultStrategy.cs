using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.BattleModule;

namespace ZombieShooter
{
    public class DefaultStrategy : WeaponPickUpStrategy
    {
        public override void Add(List<Weapon> weapons, Weapon weapon)
        {
            if (HasWeapon(weapons, weapon))
            {
                return;
            }

            weapons.Add(weapon);
            Debug.Log(weapon.Type + " added with index " + (weapons.Count - 1));
        }
    }
}
