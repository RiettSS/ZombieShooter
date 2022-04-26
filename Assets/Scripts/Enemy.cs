using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private Health _health;

    private void Awake()
    {
         _health = new Health(100, 100);
    }
    private void Update()
    {
        if (_player == null)
            return;

        var objectPos = _player.transform.position;
        var dir = objectPos - transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg));

        var rb = GetComponent<Rigidbody2D>();

        if (rb.velocity.magnitude < 1)
            rb.AddRelativeForce(new Vector2(0, 10));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Bullet bullet))
        {
            if (_health.Current < bullet.Damage)
            {
                Destroy();
            }
            else
            {
                _health = _health.TakeDamage(bullet.Damage);
                if (_health.Current <= 0)
                    Destroy();
            }
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
