using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DiceRollChanceCalculator.Constants;
using DiceRollChanceCalculator.Models;
using System.Collections.ObjectModel;

namespace DiceRollChanceCalculator.ViewModels;

public partial class CreateCalculationViewModel : ObservableObject
{
    [ObservableProperty]
    private CalculationModel _calculationModel;

    [ObservableProperty]
    public ObservableCollection<DiceDamage> _attackDamages;

    [ObservableProperty]
    public List<KeyValuePair<string, int>> _dicePickerOptions;

    [ObservableProperty]
    public bool _isCollectionViewTitlesVissible;

    public CreateCalculationViewModel()
    {
        _calculationModel = new CalculationModel();
        _attackDamages = new ObservableCollection<DiceDamage>();
        _dicePickerOptions = DiceType.GetDiceTypeDictionary().ToList();
    }

    [RelayCommand]
    public async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task AddDamageAsync()
    {
        AttackDamages.Add(new DiceDamage());
        IsCollectionViewTitlesVissible = true;
    }

    [RelayCommand]
    public async Task DeleteDiceDamageAsync(DiceDamage damage)
    {
        if(AttackDamages.Contains(damage))
        {
            AttackDamages.Remove(damage);
        }

        IsCollectionViewTitlesVissible = AttackDamages.Count > 0;
    }

    [RelayCommand]
    public async Task CalculateAsync()
    {
        CalculationModel.AttackDamages = AttackDamages.ToList();
    }

}
