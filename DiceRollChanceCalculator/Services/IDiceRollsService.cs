using DiceRollChanceCalculator.Models;

namespace DiceRollChanceCalculator.Services
{
    public interface IDiceRollsService
    {
        Task<CalculationModel> MakeCalulationAsync(CalculationModel calculation);
    }
}
