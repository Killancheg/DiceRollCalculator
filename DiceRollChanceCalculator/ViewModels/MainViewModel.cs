using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiceRollChanceCalculator.Models;
using DiceRollChanceCalculator.Services;
using DiceRollChanceCalculator.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace DiceRollChanceCalculator.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly CalculationStoringService _calculatorStoringService;

    public MainViewModel(CalculationStoringService calculationStoringService)
    {
        _calculatorStoringService = calculationStoringService;
    }

    [ObservableProperty]
    public ObservableCollection<CalculationModel> _calculations;

    [RelayCommand]
    private async Task OpenCreateCalcutionPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(CreateCalculationPage));
    }

    [RelayCommand]
    private void DeleteCalculation(CalculationModel calculation)
    {
        Calculations.Remove(calculation);

        _calculatorStoringService.DeleteCalculation(calculation);
    }

    [RelayCommand]
    private async Task OpenCalculationPageAsync(CalculationModel calculation)
    {
        await Shell.Current.GoToAsync($"{nameof(CalculationPage)}?calculation={Uri.EscapeDataString(JsonSerializer.Serialize(calculation))}");
    }

    [RelayCommand]
    private async Task GetCalculationsOnLoadAsync()
    {
        Calculations = new ObservableCollection<CalculationModel>(await _calculatorStoringService.GetAllCalcutionsAync());
    }
}
