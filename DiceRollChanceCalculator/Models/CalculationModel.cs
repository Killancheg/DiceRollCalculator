using DiceRollChanceCalculator.Models.RuleSystems;

namespace DiceRollChanceCalculator.Models;

public class CalculationModel
{
    public string Name { get; set; }

    public int AttackModifier { get; set; }

    public List<DiceDamage> AttackDamages { get; set; } = new List<DiceDamage>();

    public IRuleSystem RuleSystem { get; set; }

    public bool Simulated { get; set; }

    public List<ExpectedHitResult> HitResults { get; set; } = new List<ExpectedHitResult>();

    public double AverageDamage { get; set; }
}
