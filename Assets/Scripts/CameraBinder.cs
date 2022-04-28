using UnityEngine;
using Zenject;

public class CameraBinder : MonoBehaviour
{
    private Transform _objectToFollow;

    [Inject]
    private void Construct(Player player)
    {
        _objectToFollow = player.transform;
    }
    
    private void Update()
    {
        var position = _objectToFollow.position;
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
