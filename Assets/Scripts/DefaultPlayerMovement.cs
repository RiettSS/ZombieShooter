using UnityEngine;
using UnityEngine.UI;

public class DefaultPlayerMovement : MonoBehaviour
{
    [SerializeField] private Text _staminatextbox;

    private float _walkingSpeed = 1.5f;
    private float _runningSpeed = 2.5f;
    private float _stamina = 100;
    private float _stamingReducingSpeed = 0.2f;
    private float _staminaRegenerationSpeed = 0.2f;
    private float _currentSpeed;

    private Rigidbody2D _rb;
    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        _currentSpeed = _walkingSpeed; 
    }

    private void OnEnable()
    {
        _input.ForwardPressed += MoveForward;
        _input.BackPressed += MoveBack;
        _input.LeftPressed += MoveLeft;
        _input.RightPressed += MoveRight;
        _input.BoostPressed += Boost;
        _input.BoostUnpressed += BoostRegeneration;
    }

    private void OnDisable()
    {
        _input.ForwardPressed -= MoveForward;
        _input.BackPressed -= MoveBack;
        _input.LeftPressed -= MoveLeft;
        _input.RightPressed -= MoveRight;
        _input.BoostPressed -= Boost;
        _input.BoostUnpressed -= BoostRegeneration;
    }

    private void MoveForward()
    {
        _rb.AddForce(new Vector2(0, _currentSpeed));
    }

    private void MoveLeft()
    {
        _rb.AddForce(new Vector2(-_currentSpeed, 0));
    }

    private void MoveRight()
    {
        _rb.AddForce(new Vector2(_currentSpeed, 0));
    }

    private void MoveBack()
    {
        _rb.AddForce(new Vector2(0, -_currentSpeed));
    }

    private void Boost()
    {
        if (_stamina > 0)
        {
            _stamina -= _stamingReducingSpeed;
            _currentSpeed = _runningSpeed;
        } 
        else
        {
            _currentSpeed = _walkingSpeed;
        }
    }

    private void BoostRegeneration()
    {
        _currentSpeed = _walkingSpeed;

        if (_stamina < 100)
            _stamina += _staminaRegenerationSpeed;
    }
}
