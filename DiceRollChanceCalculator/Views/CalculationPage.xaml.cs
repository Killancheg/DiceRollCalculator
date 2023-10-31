using DiceRollChanceCalculator.ViewModels;

namespace DiceRollChanceCalculator.Views;

public partial class CalculationPage : ContentPage
{
	public CalculationPage(CalculationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}