namespace DiceRollChanceCalculator.Models;

public class DiceDamage
{
    public KeyValuePair<string, int> DiceType { get; set; }

    public int AditionalDamage { get; set; }

    public int CriticalMultiplayer { get; set; }
}
