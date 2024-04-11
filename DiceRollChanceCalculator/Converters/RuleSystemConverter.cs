using DiceRollChanceCalculator.Constants;
using DiceRollChanceCalculator.Models.RuleSystems;

namespace DiceRollChanceCalculator.Converters
{
    public static class RuleSystemConverter
    {
        public static string ConvertRuleSystem(IRuleSystem ruleSystem)
        {
            return ruleSystem switch
            {
                DnD5ERuleSystem => RuleSystem.DnD5E,
                Pathfinder1ERuleSystem => RuleSystem.Pathfinder1E,
                _ => RuleSystem.NotImplemented
            };
        }

        public static IRuleSystem ConvertRuleSystem(string ruleSystem)
        {
            return ruleSystem switch
            {
                RuleSystem.DnD5E => new DnD5ERuleSystem(),
                RuleSystem.Pathfinder1E => new Pathfinder1ERuleSystem(),
                _ => null
            };
        }
    }
}
