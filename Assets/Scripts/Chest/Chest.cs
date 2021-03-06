using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private Sprite _openedChest;
    [SerializeField] private Sprite _closedChest;
    [SerializeField] private GameObject _bonus;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isCollected;

    private void Start()
    {
        _spriteRenderer.sprite = _closedChest;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCollected)
            return;

        if(collision.gameObject.TryGetComponent(out Player player))
        {
            _spriteRenderer.sprite = _openedChest;
            Instantiate(_bonus, transform);
            _isCollected = true;
        }
    }

}
