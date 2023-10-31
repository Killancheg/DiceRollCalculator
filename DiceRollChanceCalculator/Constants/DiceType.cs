namespace DiceRollChanceCalculator.Constants;

public static class DiceType
{
    public const int D3 = 3;
    public const int D4 = 4;
    public const int D6 = 6;
    public const int D8 = 8;
    public const int D10 = 10;
    public const int D12 = 12;
    public const int D20 = 20;
    public const int D100 = 100;

    public static Dictionary<string, int> GetDiceTypeDictionary()
    {
        var DiceDictionary = new Dictionary<string, int>();

        DiceDictionary.Add("d3", D3);
        DiceDictionary.Add("d4", D4);
        DiceDictionary.Add("d6", D6);
        DiceDictionary.Add("d8", D8);
        DiceDictionary.Add("d10", D10);
        DiceDictionary.Add("d12", D12);
        DiceDictionary.Add("d20", D20);
        DiceDictionary.Add("d100", D100);

        return DiceDictionary;
    }
}
