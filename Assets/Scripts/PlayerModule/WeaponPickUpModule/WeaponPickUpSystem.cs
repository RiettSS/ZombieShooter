using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombieShooter.BattleModule;

namespace ZombieShooter
{
    public class WeaponPickUpSystem : MonoBehaviour
    {
        public event Action<Weapon> WeaponFound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Weapon weapon))
            {
                WeaponFound?.Invoke(weapon);
            }
        }
    }
}
