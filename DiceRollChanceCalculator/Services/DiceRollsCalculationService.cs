using DiceRollChanceCalculator.Constants;
using DiceRollChanceCalculator.Converters;
using DiceRollChanceCalculator.Models;
using DiceRollChanceCalculator.Models.RuleSystems;

namespace DiceRollChanceCalculator.Services;

public class DiceRollsCalculationService : IDiceRollsService
{
    private IRuleSystem RpgRuleSystem { get; set; }

    public async Task<CalculationModel> MakeCalulationAsync(CalculationModel calculation)
    {
        RpgRuleSystem = RuleSystemConverter.ConvertRuleSystem(calculation.RuleSystem);

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
            currentAc--;
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

        hitResult.Probability = ((21 - (armourClass - calculation.AttackModifier)) / 20) * 100;

        hitResult.AverageDamagePerRound = 0;

        foreach ( var damage in calculation.AttackDamages)
        {
            hitResult.AverageDamagePerRound += RpgRuleSystem.CalculateCriticalAverageDamageBonus(damage, hitResult.Probability);
        }

        hitResult.AverageDamagePerRound += calculation.AverageDamage;
        hitResult.AverageDamagePerRound *= hitResult.Probability;

        return hitResult;
    }
}
