using DiceRollChanceCalculator.ViewModels;

namespace DiceRollChanceCalculator.Views;

public partial class CreateCalculationPage : ContentPage
{
	public CreateCalculationPage(CreateCalculationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}