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
                dammageRollsResult.Add(calculation.RuleSystem.
                    SimulateAttackRollDamage(armourClass,calculation.AttackDamages,calculation.AttackModifier, _randomGenerator));
            }

            hitResult.AverageDamagePerRound = dammageRollsResult.Average();
            hitResult.Probability = dammageRollsResult.Count(num => num == 0) / dammageRollsResult.Count;

            return hitResult;
        }
    }
}
