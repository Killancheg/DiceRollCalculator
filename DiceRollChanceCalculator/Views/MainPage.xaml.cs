using DiceRollChanceCalculator.ViewModels;

namespace DiceRollChanceCalculator.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}