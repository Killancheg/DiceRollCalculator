using CoreImage;
using DiceRollChanceCalculator.Constants;
using DiceRollChanceCalculator.Models;

namespace DiceRollChanceCalculator.Services
{
    public class DiceRollsSimulationService : IDiceRollsService
    {
        private readonly Random _randomGenerator;

        public DiceRollsSimulationService(Random randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        public async Task<CalculationModel> MakeCalulationAsync(CalculationModel calculation)
        {
            calculation.HitResults = GetSimulatedDiceRollResults(calculation);
           
            return calculation;
        }

        private List<ExpectedHitResult> GetSimulatedDiceRollResults(CalculationModel calculation)
        {
            var hitResults = new List<ExpectedHitResult>();
            var currentAc = 19 + calculation.AttackModifier;

            if (currentAc <= 0)
            {
                return hitResults;
            }

            while (currentAc > (2 + calculation.AttackModifier))
            {
                hitResults.Add(GetSimulatedHitResult(currentAc, calculation));
            }

            if (hitResults.Count > 0)
            {
                calculation.AverageDamage = hitResults.Average(result => result.AverageDamagePerRound);
            }
            else
            {
                calculation.AverageDamage = 0;
            }

            hitResults.Reverse();

            return hitResults;
        }

        private ExpectedHitResult GetSimulatedHitResult(int armourClass, CalculationModel calculation)
        {
            if (armourClass <= 0)
            {
                return null;
            }

            var hitResult = new ExpectedHitResult()
            {
                ArmourClass = armourClass
            };

            var dammageRollsResult = new List<int>();

            for (int i = 0; i < 1000; i++)
            {
                if (calculation.RuleSystem == RuleSystem.Pathfinder1E)
                {
                    dammageRollsResult.Add(GetPathfinder1EAttackRollDamage(armourClass, calculation));
                }
                else if (calculation.RuleSystem == RuleSystem.DnD5E)
                {
                    dammageRollsResult.Add(GetDnD5EAttackRollDamage(armourClass, calculation));
                }
            }

            hitResult.AverageDamagePerRound = dammageRollsResult.Average();
            hitResult.Probability = dammageRollsResult.Count(num => num == 0) / dammageRollsResult.Count;

            return hitResult;
        }

        private int GetDnD5EAttackRollDamage(int armourClass, CalculationModel calculation)
        {
            var hitRoll = _randomGenerator.Next(1, 21) + calculation.AttackModifier;

            var fullDamage = 0;

            if (hitRoll >= armourClass)
            {
                if (hitRoll == (20 + calculation.AttackModifier))
                {
                    foreach (var diceDamage in calculation.AttackDamages)
                    {
                        for (int i = 0; i < diceDamage.CriticalMultiplayer; i++)
                        {
                            fullDamage = _randomGenerator.Next(1, diceDamage.DiceType.Value + 1);
                        }
                        fullDamage += diceDamage.AditionalDamage;
                    }
                }
                else
                {
                    foreach (var diceDamage in calculation.AttackDamages)
                    {
                        fullDamage += _randomGenerator.Next(1, diceDamage.DiceType.Value + 1) + diceDamage.AditionalDamage;
                    }
                }
            }

            return fullDamage;
        }

        private int GetPathfinder1EAttackRollDamage(int armourClass, CalculationModel calculation)
        {
            var hitRoll = _randomGenerator.Next(1, 21) + calculation.AttackModifier;

            var fullDamage = 0;

            if (hitRoll >= armourClass)
            {
                var isCriticalHit = (hitRoll == (20 + calculation.AttackModifier))
                    && ((_randomGenerator.Next(1, 21) + calculation.AttackModifier) >= armourClass);


                if (isCriticalHit)
                {
                    foreach (var diceDamage in calculation.AttackDamages)
                    {
                        for (int i = 0; i < diceDamage.CriticalMultiplayer; i++)
                        {
                            fullDamage = _randomGenerator.Next(1, diceDamage.DiceType.Value + 1) + diceDamage.AditionalDamage;
                        }
                    }
                }
                else
                {
                    foreach (var diceDamage in calculation.AttackDamages)
                    {
                        fullDamage += _randomGenerator.Next(1, diceDamage.DiceType.Value + 1) + diceDamage.AditionalDamage;
                    }
                }
            }

            return fullDamage;
        }
    }
}
