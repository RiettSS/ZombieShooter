using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ZombieShooter.BattleModule.Impl;
using ZombieShooter.PlayerModule;

public class StateBar : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _manaBar;

    private float _healthFill;
    private float _manaFill;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }
    
    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _player.ManaChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.ManaChanged -= OnHealthChanged;
    } 
    
    private void Start()
    {
        _healthFill = 1;
        _manaFill = 1;
    }

    private void Update()
    {
        _healthBar.fillAmount = _healthFill;
        _manaBar.fillAmount = _manaFill;
    }

    private void OnHealthChanged(Health health)
    {
        _healthFill = health.Amount / health.Max;
        _healthBar.fillAmount = _healthFill;
    }

    private void OnHealthChanged(Mana mana)
    {
        _manaFill = mana.Amount / mana.Max;
        _manaBar.fillAmount = _manaFill;
    }
}
