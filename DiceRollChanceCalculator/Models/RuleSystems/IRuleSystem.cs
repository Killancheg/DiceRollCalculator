using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceRollChanceCalculator.Models.RuleSystems
{
    public interface IRuleSystem
    {
        double CalculateCriticalAverageDamageBonus(DiceDamage damage, double probability);

        int SimulateAttackRollDamage(int armourClass, List<DiceDamage> damages, int attackModifier, Random randomGenerator);
    }
}
