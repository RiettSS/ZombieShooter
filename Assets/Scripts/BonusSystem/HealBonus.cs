using UnityEngine;

public class HealBonus : Bonus
{
    [SerializeField] private float _hpToAdd;

    public Health Health => new Health(_hpToAdd, int.MaxValue);
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out IBonusVisitor bonusVisitor))
        {
            bonusVisitor.Visit(this);
        }
    }
}
