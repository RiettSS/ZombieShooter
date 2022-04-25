using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBinder : MonoBehaviour
{
    [SerializeField] private Transform _objectToFollow;

    private void Update()
    {
        transform.position = _objectToFollow.position;
        transform.position += new Vector3(0, 0, -10);
    }
}
