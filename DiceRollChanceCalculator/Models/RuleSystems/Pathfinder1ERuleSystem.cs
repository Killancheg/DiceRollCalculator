﻿namespace DiceRollChanceCalculator.Models.RuleSystems
{
    public class Pathfinder1ERuleSystem : IRuleSystem
    {
        public double CalculateCriticalAverageDamageBonus(DiceDamage damage, double probability)
        {
            return (1 / 20) * probability * damage.CriticalMultiplayer * ((damage.DiceType.Value + 1 + damage.AditionalDamage) / 2);
        }

        public int SimulateAttackRollDamage(int armourClass, List<DiceDamage> damages, int attackModifier, Random randomGenerator)
        {
            var hitRoll = randomGenerator.Next(1, 21) + attackModifier;

            var fullDamage = 0;

            if (hitRoll >= armourClass)
            {
                var isCriticalHit = (hitRoll == (20 + attackModifier))
                    && ((randomGenerator.Next(1, 21) + attackModifier) >= armourClass);


                if (isCriticalHit)
                {
                    foreach (var diceDamage in damages)
                    {
                        for (int i = 0; i < diceDamage.CriticalMultiplayer; i++)
                        {
                            fullDamage = randomGenerator.Next(1, diceDamage.DiceType.Value + 1) + diceDamage.AditionalDamage;
                        }
                    }
                }
                else
                {
                    foreach (var diceDamage in damages)
                    {
                        fullDamage += randomGenerator.Next(1, diceDamage.DiceType.Value + 1) + diceDamage.AditionalDamage;
                    }
                }
            }

            return fullDamage;
        }
    }
}
