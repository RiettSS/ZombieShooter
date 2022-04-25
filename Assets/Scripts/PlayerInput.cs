using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action ForwardPressed;
    public event Action BackPressed;
    public event Action LeftPressed;
    public event Action RightPressed;

    public event Action BoostPressed;
    public event Action BoostUnpressed;
    public event Action Fire;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            BoostPressed?.Invoke();
        } else
        {
            BoostUnpressed?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
            Fire?.Invoke();

        if (Input.GetKey(KeyCode.Mouse1))
            Fire?.Invoke();

        if (Input.GetKey(KeyCode.D)) 
            RightPressed?.Invoke();

        if (Input.GetKey(KeyCode.A))
            LeftPressed?.Invoke();

        if (Input.GetKey(KeyCode.W))
            ForwardPressed?.Invoke();

        if (Input.GetKey(KeyCode.S))
            BackPressed?.Invoke();
    }
}
