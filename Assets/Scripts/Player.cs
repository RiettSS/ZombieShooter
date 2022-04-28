using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IBonusVisitor, IDamagable
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Text _hpText;

    private Health Health;
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _input.Fire += Shoot;

        Health = new Health(100, 100);
    }

    private void Start()
    {
        _hpText.text = Health.Current.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bonus bonus))
        {
            bonus.Apply(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {

            if (Health.Current == 0)
                Die();
        }
    }

    private void Shoot()
    {
        _weapon.Shoot();
    }

    private void AddHealth(float hp)
    {
        Health = Health.AddHealth(hp);
        _hpText.text = Health.Current.ToString();
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
    public void Visit(HealBonus bonus, float hpToHeal)
    {
        AddHealth(hpToHeal);
    }

    public void Visit(DamageMultiplierBonus bonus, float multiplier, float bonusDuration)
    {
        StartCoroutine(DamageMultiplier(multiplier, bonusDuration));
    }

    private IEnumerator DamageMultiplier(float multiplier, float bonusDuration)
    {
        _weapon.Damage = new Damage(_weapon.Damage.Amount * multiplier);
        yield return new WaitForSeconds(bonusDuration);
        _weapon.Damage = new Damage(_weapon.Damage.Amount / multiplier);
    }

    public void ApplyDamage(Damage damage)
    {
        Health = Health.TakeDamage(damage);
        _hpText.text = Health.Current.ToString();

        if (Health.IsEmpty)
            Die();
    }
}


[Serializable]
public struct Health
{
    private const float Min = 0;
    [SerializeField] public readonly float Current;
    [SerializeField] private readonly float Max;
    public bool IsEmpty { get; }
    public Health(float hp, float max)
    {
        if (hp - max > 0)
            throw new ArgumentException();
            
        if (hp < Min)
            throw new ArgumentException();

        if (max < 0)
            throw new ArgumentException();

        Current = hp;
        Max = max;
        IsEmpty = hp < Mathf.Epsilon;
    }

    public Health TakeDamage(Damage damage)
    {
        if (Current - damage.Amount < Mathf.Epsilon)
        {
            return new Health(0, Max);
        }
        else
        {
            return new Health(Current - damage.Amount, Max);
        }
    }

    public Health AddHealth(float hp)
    {
        if (Current + hp >= Max)
        {
            return new Health(Max, Max);
        }
        else
        {
            return new Health(Current + hp, Max);
        }
    }
}
