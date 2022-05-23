using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieShooter
{
    public class TeleportMovement : Movement
    {
        private readonly Transform _transform;

        public TeleportMovement(Transform transform)
        {
            _transform = transform;
        }

        public override void Move(Vector2 direction)
        {
            var endPoint = direction * _currentSpeed * Time.deltaTime * 0.1f;
            _transform.position = new Vector3(_transform.position.x + endPoint.x, _transform.position.y + endPoint.y, _transform.position.z);
        }
    }
}
