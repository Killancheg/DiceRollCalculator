namespace DiceRollChanceCalculator.Models.RuleSystems
{
    public class Pathfinder1ERuleSystem : IRuleSystem
    {
        public double CalculateCriticalAverageDamageBonus(DiceDamage damage, double probability)
        {
            return (1 / 20) * probability * damage.CriticalMultiplayer * ((damage.DiceType.Value + 1 + damage.AditionalDamage) / 2);
        }

        public int SimulateAttackRollDamage(int armourClass, List<DiceDamage> damages, int attackModifier, Random randomGenerator)
        {
            var hitRoll = randomGenerator.Next(1, 20);

            var fullDamage = 0;

            if ((hitRoll + attackModifier) >= armourClass && hitRoll != 1)
            {
                var isCriticalHit = (hitRoll == 20
                    && (randomGenerator.Next(1, 20) + attackModifier >= armourClass));


                if (isCriticalHit)
                {
                    foreach (var diceDamage in damages)
                    {
                        for (int i = 0; i < diceDamage.CriticalMultiplayer; i++)
                        {
                            fullDamage += randomGenerator.Next(1, diceDamage.DiceType.Value) + diceDamage.AditionalDamage;
                        }
                    }
                }
                else
                {
                    foreach (var diceDamage in damages)
                    {
                        fullDamage += randomGenerator.Next(1, diceDamage.DiceType.Value) + diceDamage.AditionalDamage;
                    }
                }
            }

            return fullDamage;
        }
    }
}
