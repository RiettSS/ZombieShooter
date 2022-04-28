using UnityEngine;

namespace ZombieShooter.PlayerModule
{
    public class PlayerToMouseRotation : MonoBehaviour
    {
    
        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var objectPos = Camera.main.WorldToScreenPoint(transform.position);
            var dir = Input.mousePosition - objectPos;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg));
        }
    }
}
