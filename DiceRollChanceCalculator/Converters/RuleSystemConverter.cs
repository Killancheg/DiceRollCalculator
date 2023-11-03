using DiceRollChanceCalculator.Constants;
using DiceRollChanceCalculator.Models.RuleSystems;

namespace DiceRollChanceCalculator.Converters
{
    public static class RuleSystemConverter
    {
        public static string ConvertRuleSystem(IRuleSystem ruleSystem)
        {
            switch (ruleSystem)
            {
                case DnD5ERuleSystem:
                    return RuleSystem.DnD5E;
                case Pathfinder1ERuleSystem:
                    return RuleSystem.Pathfinder1E;
                default:
                    return RuleSystem.NotImplemented;
            }
        }

        public static IRuleSystem ConvertRuleSystem(string ruleSystem)
        {
            switch (ruleSystem)
            {
                case RuleSystem.DnD5E:
                    return new DnD5ERuleSystem();
                case RuleSystem.Pathfinder1E:
                    return new Pathfinder1ERuleSystem();
                default:
                    return null;
            }
        }
    }
}
