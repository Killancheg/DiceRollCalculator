using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiceRollChanceCalculator.Views;

namespace DiceRollChanceCalculator.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    private async Task OpenCreateCalcutionPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateCalculationPage));
    }
}
