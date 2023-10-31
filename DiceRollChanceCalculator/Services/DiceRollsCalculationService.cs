using DiceRollChanceCalculator.Constants;
using DiceRollChanceCalculator.Models;

namespace DiceRollChanceCalculator.Services;

public class DiceRollsCalculationService : IDiceRollsService
{
    private readonly Random _randomGenerator;

    public DiceRollsCalculationService(Random randomGenerator)
    {
        _randomGenerator = randomGenerator;
    }

    public async Task<CalculationModel> MakeCalulationAsync(CalculationModel calculation)
    {
        
        calculation.AverageDamage = GetCalculatedFullAveregeDamage(calculation);
        calculation.HitResults = GetCalculatedHitResults(calculation);

        return calculation;
    }

    private double GetCalculatedFullAveregeDamage(CalculationModel calculation)
    {
        double AverageDamage = 0;

        foreach (var damage in calculation.AttackDamages)
        {
            AverageDamage += (damage.DiceType.Value / 2) + damage.AditionalDamage;
        }

        return AverageDamage;
    }

    private List<ExpectedHitResult> GetCalculatedHitResults(CalculationModel calculation)
    {
        var hitResults = new List<ExpectedHitResult>();
        var currentAc = 19 + calculation.AttackModifier;

        if (currentAc <= 0)
        {
            return hitResults;
        }

        while (currentAc > (2 + calculation.AttackModifier))
        {
            hitResults.Add(GetCalculatedHitResult(currentAc, calculation));
        }

        hitResults.Reverse();

        return hitResults;
    }


    private ExpectedHitResult GetCalculatedHitResult(int armourClass, CalculationModel calculation)
    {
        if (armourClass <= 0)
        {
            return null;
        }

        var hitResult = new ExpectedHitResult()
        {
            ArmourClass = armourClass
        };

        hitResult.Probability = (21 - (armourClass - calculation.AttackModifier)) / 20;

        hitResult.AverageDamagePerRound = 0;

        foreach ( var damage in calculation.AttackDamages)
        {
            hitResult.AverageDamagePerRound += GetCalculatedCriticalAverageDamageBonus(damage, hitResult.Probability, calculation.RuleSystem);
        }

        hitResult.AverageDamagePerRound += calculation.AverageDamage;
        hitResult.AverageDamagePerRound *= hitResult.Probability;

        return hitResult;
    }

    private double GetCalculatedCriticalAverageDamageBonus(DiceDamage damage, double probability, string ruleSystem)
    {
        if (ruleSystem == RuleSystem.DnD5E)
        {
            return (1 / 20) * damage.CriticalMultiplayer * ((damage.DiceType.Value + 1) / 2);
        }
        else if (ruleSystem == RuleSystem.Pathfinder1E)
        {
            return (1 / 20) * probability * damage.CriticalMultiplayer * ((damage.DiceType.Value + 1 + damage.AditionalDamage) / 2);
        }

        return 0;
    }
}
