using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookDirection : MonoBehaviour
{
    [SerializeField] private Transform _playerSprite;   
    private bool facingRight = false;

    private Vector3 pos;
    public void Update()
    {
        LookAtCursor();
        pos = Camera.main.WorldToScreenPoint(transform.position);
    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = _playerSprite.transform.localScale;
        Scaler.x *= -1;
        _playerSprite.transform.localScale = Scaler;
    }

    public void LookAtCursor()
    {
        if (Input.mousePosition.x < pos.x && !facingRight) Flip();
        else if (Input.mousePosition.x > pos.x && facingRight) Flip();
    }
}
