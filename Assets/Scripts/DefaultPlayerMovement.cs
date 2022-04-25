using UnityEngine;
using UnityEngine.UI;

public class DefaultPlayerMovement : MonoBehaviour
{
    [SerializeField] private Text _staminatextbox;

    private float _walkingSpeed = 0.008f;
    private float _runningSpeed = 0.014f;
    private float _stamina = 100;
    private float _stamingReducingSpeed = 0.2f;
    private float _staminaRegenerationSpeed = 0.2f;

    private float _currentSpeed;


    private PlayerInput _input;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
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
        transform.position -= new Vector3(0, -_currentSpeed);
    }

    private void MoveLeft()
    {
        transform.position -= new Vector3(_currentSpeed, 0);
    }

    private void MoveRight()
    {
        transform.position += new Vector3(_currentSpeed, 0);
    }

    private void MoveBack()
    {
        transform.position += new Vector3(0, -_currentSpeed);
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
