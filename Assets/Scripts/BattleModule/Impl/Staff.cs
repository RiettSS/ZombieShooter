using UnityEngine;
using Zenject;

namespace ZombieShooter.BattleModule.Impl
{
    public class Staff : Weapon
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _bulletSpawner;

        private float _ammoRegenerationAmountPerTime = 1;
        private float _ammoRegenerationDelayInSeconds = 1;
        private float _nextRegenerationTime = 0;

        private Camera _camera; 
    
        private void Awake()
        {
            MaxAmmo = 10;
            CurrentAmmo = 5;
            Damage = new Damage(20);
            Type = WeaponType.Staff;
        }

        [Inject]
        private void Construct(Camera cam)
        {
            _camera = cam;
        }

        public override void Shoot()
        {
            if (CurrentAmmo <= 0)
                return;

            var worldMousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
            var direction = (worldMousePosition - transform.position).normalized;
            var bullet = Instantiate(_bullet, _bulletSpawner.position, Quaternion.identity, null);
            bullet.Initialize(Damage);
            bullet.transform.parent = null;
            var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
        
            bulletRigidBody.velocity = 50 * direction;

            CurrentAmmo -= 1;
        }

        private void Update()
        {
            AmmoRegeneration();
        }

        private void AmmoRegeneration()
        {
            if (CurrentAmmo >= MaxAmmo)
                return;
        
            if (Time.time > _nextRegenerationTime)
            {
                _nextRegenerationTime = Time.time + _ammoRegenerationDelayInSeconds;
                CurrentAmmo += _ammoRegenerationAmountPerTime;
            }
        }
    }
}
