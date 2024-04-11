using CommunityToolkit.Mvvm.ComponentModel;
using DiceRollChanceCalculator.Converters;
using DiceRollChanceCalculator.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace DiceRollChanceCalculator.ViewModels;

[QueryProperty("CalculationModelParameter", "model")]
public partial class CalculationViewModel : ObservableObject
{
    [ObservableProperty]
    private CalculationModel _calculationModel;

    [ObservableProperty]
    public ObservableCollection<DiceDamage> _attackDamages;

    [ObservableProperty]
    public ObservableCollection<ExpectedHitResult> _hitResults;

    [ObservableProperty]
    public string _ruleSystem;

    private string CalculationModelParameter
    {
        set
        {
            CalculationModel = JsonSerializer.Deserialize<CalculationModel>(Uri.UnescapeDataString(value));
            AttackDamages = new ObservableCollection<DiceDamage>(CalculationModel.AttackDamages);
            HitResults = new ObservableCollection<ExpectedHitResult>(CalculationModel.HitResults);
            RuleSystem = RuleSystemConverter.ConvertRuleSystem(CalculationModel.RuleSystem);
        }
    }
}
