using UnityEngine;
using Zenject;

namespace ZombieShooter.PlayerModule
{
    public class LookDirectionChanger : MonoBehaviour
    {
        private bool _facingRight;
        private Vector3 _pos;
        private Camera _camera;
    
        [Inject]
        private void Construct(Camera cam)
        {
            _camera = cam;
        }
    
        public void Update()
        {
            LookAtCursor();
            _pos = _camera.WorldToScreenPoint(transform.position);
        }
    
        private void LookAtCursor()
        {
            if (Input.mousePosition.x < _pos.x && !_facingRight) Flip();
            else if (Input.mousePosition.x > _pos.x && _facingRight) Flip();
        }

        private void Flip()
        {
            _facingRight = !_facingRight;
            var scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
}
