namespace DiceRollChanceCalculator.Models.RuleSystems
{
    public interface IRuleSystem
    {
        double CalculateCriticalAverageDamageBonus(DiceDamage damage, double probability);

        int SimulateAttackRollDamage(int armourClass, List<DiceDamage> damages, int attackModifier, Random randomGenerator);
    }
}
