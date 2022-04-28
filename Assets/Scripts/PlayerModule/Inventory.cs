using System;
using System.Collections.Generic;
using ZombieShooter.BattleModule;

namespace ZombieShooter
{
    public class Inventory
    {
        public event Action<Weapon> WeaponSwitched;

        private const int DEFAULT_INVENTORY_CAPACITY = 3;

        private List<Weapon> _weapons;
        private int _activeSlotIndex;

        private WeaponPickUpStrategy _weaponPickUpStrategy;

        public Inventory(WeaponPickUpStrategy weaponPickUpStrategy)
        {
            _weaponPickUpStrategy = weaponPickUpStrategy;
            _weapons = new List<Weapon>(DEFAULT_INVENTORY_CAPACITY);
        }

        public void Add(Weapon weapon)
        {
            _weaponPickUpStrategy.Add(_weapons, weapon);
        }

        public void Remove(int slotNumber)
        {
            _weapons.RemoveAt(slotNumber);
        }

        public void WeaponOne()
        {
            Switch(0);
        }

        public void WeaponTwo()
        {
            Switch(1);
        }

        public void WeaponThree()
        {
            Switch(2);
        }

        public void Switch(int index)
        {
            if (index >= 0 && index < _weapons.Count)
            {
                foreach(var weapon in _weapons)
                {
                    weapon.gameObject.SetActive(false);
                }

                _weapons[index].gameObject.SetActive(true);

                _activeSlotIndex = index;
                WeaponSwitched?.Invoke(_weapons[index]);
            }
        }

        private void InitializeInventory()
        {
            for (var i = 0; i < DEFAULT_INVENTORY_CAPACITY; i++)
            {
                _weapons.Add(null);
            }
        }
    }
}
