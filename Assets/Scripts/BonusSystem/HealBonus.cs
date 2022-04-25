using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : Bonus
{
    [SerializeField] private float _hpToAdd;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out IBonusVisitor bonusVisitor))
        {
            Accept(bonusVisitor, _hpToAdd);
        }
    }

    public void Accept(IBonusVisitor bonusVisitor, float hpToAdd)
    {
        bonusVisitor.Visit(this, hpToAdd);

        Destroy(gameObject);
    }


}
