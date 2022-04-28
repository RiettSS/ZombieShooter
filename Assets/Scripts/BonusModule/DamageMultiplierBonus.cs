using UnityEngine;

namespace ZombieShooter.BonusModule
{
    public class DamageMultiplierBonus : Bonus
    {
        [SerializeField] private float _multiplier;
        [SerializeField] private float _bonusDuration;

        public float Multiplier => _multiplier;
        public float BonusDuration => _bonusDuration;
    
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out IBonusVisitor bonusVisitor))
            {
                bonusVisitor.Visit(this);
            }
        }
    }
}
