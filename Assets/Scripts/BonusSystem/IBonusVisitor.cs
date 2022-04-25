using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonusVisitor 
{
    void Visit(HealBonus bonus, float hpToHeal);
    void Visit(DamageMultiplierBonus bonus, float multiplier, float duration);
}
