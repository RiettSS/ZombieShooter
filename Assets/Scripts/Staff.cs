using UnityEngine;
using UnityEngine.UI;

public class Staff : Weapon
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletSpawner;
    [SerializeField] private Text _mana;

    private float _ammoRegenerationAmount = 1; //ammo per time
    private float _ammoRegenerationDelay = 1; //ms
    
    private float _nextRegenerationTime = 0;

    private void Awake()
    {
        MaxAmmo = 10;
        CurrentAmmo = 5;
        Damage = new Damage(20);
    }

    public override void Shoot()
    {
        if (CurrentAmmo <= 0)
            return;

        var bullet = Instantiate(_bullet, _bulletSpawner);
        bullet.Initialize(Damage);
        bullet.transform.parent = null;
        var bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidBody.AddRelativeForce(new Vector2(850, 0));

        CurrentAmmo -= 1;
    }
   
    private void Update()
    {
        AmmoRegeneration();

        _mana.text = CurrentAmmo.ToString();
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
