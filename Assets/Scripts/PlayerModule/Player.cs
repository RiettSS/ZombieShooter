using System;
using System.Collections;
using UnityEngine;
using Zenject;
using ZombieShooter.BattleModule;
using ZombieShooter.BattleModule.Impl;
using ZombieShooter.BonusModule;
using ZombieShooter.Service;

namespace ZombieShooter.PlayerModule
{
    public class Player : MonoBehaviour, IBonusVisitor, IDamageable
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Health _health;
        private Movement _movement;
        private Inventory _inventory;
        private WeaponPickUpSystem _weaponPickUpSystem;
        private IInputService _inputService;
    
        public event Action<Health> HealthChanged;
        public event Action<Mana> ManaChanged;

        private float ShootingDelayAfterWeaponSwitchInSeconds = 0.5f;
        
        private void Awake()
        {
            _health = new Health(100, 100);
            _movement = new Movement(_rigidbody2D);
            _inventory = new Inventory(new SpecifiedStrategy());
            _weaponPickUpSystem = GetComponent<WeaponPickUpSystem>();
        }
    
        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void OnEnable()
        { 
            _inputService.Fire += _weapon.Shoot;
            _inputService.MovementDirectionUpdated += _movement.Move;
            _inputService.BoostPressed += _movement.Boost;
            _inputService.BoostUnpressed += _movement.BoostRegeneration;
            _inputService.WeaponOnePressed += _inventory.WeaponOne;
            _inputService.WeaponTwoPressed += _inventory.WeaponTwo;
            _inputService.WeaponThreePressed += _inventory.WeaponThree;
            _weaponPickUpSystem.WeaponFound += _inventory.Add;
            _weaponPickUpSystem.WeaponFound += SetPlayerAsWeaponsParent;
            _inventory.WeaponSwitched += SwitchWeapon;
        }

        private void OnDisable()
        { 
            _inputService.Fire -= _weapon.Shoot;
            _inputService.MovementDirectionUpdated -= _movement.Move;
            _inputService.BoostPressed -= _movement.Boost;
            _inputService.BoostUnpressed -= _movement.BoostRegeneration;
            _inputService.WeaponOnePressed -= _inventory.WeaponOne;
            _inputService.WeaponTwoPressed -= _inventory.WeaponTwo;
            _inputService.WeaponThreePressed -= _inventory.WeaponThree;
            _weaponPickUpSystem.WeaponFound -= _inventory.Add;
            _weaponPickUpSystem.WeaponFound -= SetPlayerAsWeaponsParent;
            _inventory.WeaponSwitched -= SwitchWeapon;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    
        void IBonusVisitor.Visit(HealBonus healthBonus)
        {
            _health = _health.AddHealth(healthBonus.Health);
            HealthChanged?.Invoke(_health);
        }

        void IBonusVisitor.Visit(DamageMultiplierBonus bonus)
        {
            StartCoroutine(DamageMultiplier(bonus.Multiplier, bonus.BonusDuration));
        }

        private IEnumerator DamageMultiplier(float multiplier, float bonusDuration)
        {
            _weapon.Damage = new Damage(_weapon.Damage.Amount * multiplier);
            yield return new WaitForSeconds(bonusDuration);
            _weapon.Damage = new Damage(_weapon.Damage.Amount / multiplier);
        }

        private IEnumerator ShootingBlocker(float duration)
        {
            _inputService.Fire -= _weapon.Shoot;
            yield return new WaitForSeconds(duration);
            _inputService.Fire += _weapon.Shoot;
        }

        public void ApplyDamage(Damage damage)
        {
            _health = _health.TakeDamage(damage);
            HealthChanged?.Invoke(_health);
            if (_health.IsEmpty)
                Die();
        }

        private void SwitchWeapon(Weapon weapon)
        {
            _inputService.Fire -= _weapon.Shoot;
            _weapon = weapon;
            _inputService.Fire += _weapon.Shoot;

            StartCoroutine(ShootingBlocker(ShootingDelayAfterWeaponSwitchInSeconds));
        }

        private void SetPlayerAsWeaponsParent(Weapon weapon)
        {
            weapon.transform.SetParent(gameObject.transform);
        }
    }
}