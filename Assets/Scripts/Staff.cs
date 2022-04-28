﻿using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class Staff : Weapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletSpawner;

    private float _ammoRegenerationAmount = 1; //ammo per time
    private float _ammoRegenerationDelay = 1; //ms
    private float _nextRegenerationTime = 0;

    private Player _player;

    private Camera _camera; 
    
    private void Awake()
    {
        MaxAmmo = 10;
        CurrentAmmo = 5;
        Damage = new Damage(20);

        _player = GetComponent<Player>();
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
        var bullet = Instantiate(_bullet, _bulletSpawner.position, quaternion.identity, null);
        bullet.Initialize(Damage);
        bullet.transform.parent = null;
        var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
        
        bulletRigidBody.velocity = 50 * direction;

        //CurrentAmmo -= 1;
        
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
            _nextRegenerationTime = Time.time + _ammoRegenerationDelay;
            CurrentAmmo += _ammoRegenerationAmount;
        }
    }
}
