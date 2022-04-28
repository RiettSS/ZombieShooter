using UnityEngine;

public class Knife : Weapon
{
    [SerializeField] private float _damage;

    private void Start()
    {
        Damage = new Damage(_damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("damage applied");
            player.ApplyDamage(Damage);
        }
    }
}
