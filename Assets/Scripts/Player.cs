using System;
using System.Collections;
using UnityEngine;
using Zenject;
using ZombieShooter.Service;

public class Player : MonoBehaviour, IBonusVisitor, IDamagable
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private Health _health;
    private Mana _mana;
    private Movement _movement;
    private IInputService _inputService;

    public event Action<Health> HealthChanged;
    private void Awake()
    {
        _health = new Health(100, 100);
        _mana = new Mana(100, 100);
        _movement = new Movement(_rigidbody2D);
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bonus bonus))
        {
            bonus.Apply(this);
        }
    }
    
    private void Die()
    {
        gameObject.SetActive(false);
    }
    
    void IBonusVisitor.Visit(HealBonus bonus)
    {
        _health = _health.AddHealth(bonus.Health);
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