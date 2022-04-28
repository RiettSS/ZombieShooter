using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _agressiveModeDistance;
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private SpriteRenderer _sprite;

    private Player _player;
    private Health _health;
    private Damage _damage;
    private bool _facingRight = true;
    
    private void Awake()
    {
        _health = new Health(100, 100);
        _damage = new Damage(20);
    }

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    
    private void Update()
    {
        if (_player == null)
            return;

        if (Vector3.Distance(transform.position, _player.transform.position) > _agressiveModeDistance)
            return;

        var objectPos = _player.transform.position;
        var dir = objectPos - transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg));

        if (_rigidBody.velocity.magnitude < 1)
            _rigidBody.AddRelativeForce(new Vector2(0, 10));

        var angle = transform.rotation.z;

        if (_facingRight && angle > 0)
        {
            Flip();
            _facingRight = false;
        }
        if (!_facingRight && angle < 0)
        {
            Flip();
            _facingRight = true;
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
        Vector3 Scaler = _sprite.transform.localScale;
        Scaler.y *= -1;
        _sprite.transform.localScale = Scaler;
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void ApplyDamage(Damage damage)
    {
        _health = _health.TakeDamage(damage);
        if (_health.Current <= 0)
            Destroy();
    }
}
