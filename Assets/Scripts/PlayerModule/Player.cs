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
        private IMovement _movement;
        private IInputService _inputService;
    
        public event Action<Health> HealthChanged;
        public event Action<Mana> ManaChanged;
        
        private void Awake()
        {
            _health = new Health(100, 100);
            _movement = new RigidBodyMovement(_rigidbody2D);
            //_movement = new DoTweenMovement(transform);
            //_movement = new TeleportMovement(transform);
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
        }

        private void OnDisable()
        { 
            _inputService.Fire -= _weapon.Shoot;
            _inputService.MovementDirectionUpdated -= _movement.Move;
            _inputService.BoostPressed -= _movement.Boost;
            _inputService.BoostUnpressed -= _movement.BoostRegeneration;
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

        public void ApplyDamage(Damage damage)
        {
            _health = _health.TakeDamage(damage);
            HealthChanged?.Invoke(_health);
            if (_health.IsEmpty)
                Die();
        }
    }
}