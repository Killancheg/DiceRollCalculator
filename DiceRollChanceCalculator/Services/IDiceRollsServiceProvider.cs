namespace DiceRollChanceCalculator.Services
{
    public interface IDiceRollsServiceProvider
    {
        IDiceRollsService GetService(bool isSimulation);
    }
}
