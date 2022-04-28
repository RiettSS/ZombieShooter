using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
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
        _player.ManaChanged += UpdateManaBar;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= UpdateHealthBar;
        _player.ManaChanged -= UpdateManaBar;
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

    private void UpdateManaBar(Mana mana)
    {
        _manaFill = mana.Amount / mana.Max;
        _manaBar.fillAmount = _manaFill;
    }

    
}
