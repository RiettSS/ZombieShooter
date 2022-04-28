using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    [SerializeField] public Image _healthBar;
    [SerializeField] private Image _manaBar;

    private float _healthFill;
    private float _manaFill;

    private Player _player;
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.HealthChanged += UpdateHealthBar;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= UpdateHealthBar;
    }
    private void Start()
    {
        _healthFill = 1f;
        _manaFill = 1f;
    }

    private void Update()
    {
        _healthBar.fillAmount = _healthFill;
        _manaBar.fillAmount = _manaFill;
    }

    private void UpdateHealthBar(Health health)
    {
        _healthFill = health.Amount / health.Max;
        _healthBar.fillAmount = _healthFill;
    }

    
}
