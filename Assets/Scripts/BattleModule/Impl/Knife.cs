using UnityEngine;
using ZombieShooter.EnemyModule;
using ZombieShooter.PlayerModule;

namespace ZombieShooter.BattleModule.Impl
{
    public class Knife : Weapon
    {
        [SerializeField] private float _damage;

        private void Awake()
        {
            Type = WeaponType.Knife;
        }

        private void Start()
        {
            Damage = new Damage(_damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(Damage);
            }
        }
    }
}
