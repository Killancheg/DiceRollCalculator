using CommunityToolkit.Mvvm.ComponentModel;

namespace DiceRollChanceCalculator.ViewModels;

[QueryProperty("CalculationId","calculationId")]
public partial class CalculationViewModel : ObservableObject
{
    [ObservableProperty]
    private string calculationId;
}
