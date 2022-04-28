using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBonusVisitor 
{
    void Visit(HealBonus bonus);
    void Visit(DamageMultiplierBonus bonus);
}
