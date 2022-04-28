using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Damage Damage;

    public void Initialize(Damage damage)
    {
        Damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.ApplyDamage(Damage);
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
