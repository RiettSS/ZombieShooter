using UnityEngine;

public class DamageMultiplierBonus : Bonus
{
    [SerializeField] private float _multiplier;
    [SerializeField] private float _bonusDuration;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IBonusVisitor bonusVisitor))
        {
            Accept(bonusVisitor, _multiplier, _bonusDuration);
        }
    }

    public void Accept(IBonusVisitor bonusVisitor, float multiplier, float duration)
    {
        bonusVisitor.Visit(this, multiplier, duration);

        Destroy(gameObject);
    }
}
