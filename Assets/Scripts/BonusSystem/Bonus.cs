using UnityEngine;

public abstract class Bonus : MonoBehaviour, IBonus
{
    public void Apply(Player player) { }

    //public abstract void Accept(IBonusVisitor bonusVisitor);
}
