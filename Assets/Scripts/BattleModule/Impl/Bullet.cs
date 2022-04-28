using UnityEngine;
using ZombieShooter.EnemyModule;
using ZombieShooter.PlayerModule;

namespace ZombieShooter.BattleModule.Impl
{
    public class Bullet : MonoBehaviour, IBullet
    {
        private Damage _damage;

        public void Initialize(Damage damage)
        {
            _damage = damage;
        }
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out Enemy enemy))
            {
                enemy.ApplyDamage(_damage);
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                return;

            Destroy(gameObject);
        }
    }
}
