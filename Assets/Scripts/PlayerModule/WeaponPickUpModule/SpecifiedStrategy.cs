using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.BattleModule;

namespace ZombieShooter
{
    public class SpecifiedStrategy : WeaponPickUpStrategy
    {
        public override void Add(List<Weapon> weapons, Weapon weapon)
        {
            if (HasWeapon(weapons, weapon))
            {
                return;
            }

            weapons.Add(weapon);
            Sort(weapons);
        }

        private void Sort(List<Weapon> weapons)
        {
            for (int i = 0; i < weapons.Count -1; i++)
            {
                if((int)weapons[i].Type > (int)weapons[i + 1].Type)
                {
                    var buffer = weapons[i];
                    var buffer2 = weapons[i + 1];
                    weapons[i] = buffer2;
                    weapons[i + 1] = buffer;
                    Sort(weapons);
                }
            }
        }
    }
}
