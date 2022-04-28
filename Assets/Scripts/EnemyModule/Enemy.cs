using UnityEngine;
using Zenject;
using ZombieShooter.BattleModule;
using ZombieShooter.BattleModule.Impl;
using ZombieShooter.PlayerModule;

namespace ZombieShooter.EnemyModule
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _agressiveModeDistance;
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private SpriteRenderer _sprite;

        private Player _player;
        private Movement _movement;
        private Health _health;
        private Damage _damage;
        private bool _facingRight = true;

        private void Awake()
        {
            _health = new Health(100, 100);
            _movement = new Movement(_rigidBody, _agressiveModeDistance);
            _damage = new Damage(20);
        }

        [Inject]
        public void Construct(Player player)
        {
            _player = player;
        }
    
        private void Update()
        {
            _movement.TryMoveTo(_player.transform);
            
            var angle = transform.rotation.z;

            if (_facingRight && angle > 0)
            {
                Flip();
            }
            if (!_facingRight && angle < 0)
            {
                Flip();
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.ApplyDamage(_damage);
            }
        }

        private void Flip()
        {
            var scaler = _sprite.transform.localScale;
            scaler.y *= -1;
            _sprite.transform.localScale = scaler;
            _facingRight = !_facingRight;
        }
        
        private void Destroy()
        {
            Destroy(gameObject);
        }

        public void ApplyDamage(Damage damage)
        {
            _health = _health.TakeDamage(damage);
            if (_health.IsEmpty)
            {
                Destroy();
            }
        }
    }
}
