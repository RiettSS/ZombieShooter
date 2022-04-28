namespace ZombieShooter.BonusModule
{
    public interface IBonusVisitor 
    {
        void Visit(HealBonus bonus);
        void Visit(DamageMultiplierBonus bonus);
    }
}
