using DiceRollChanceCalculator.Views;

namespace DiceRollChanceCalculator
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CreateCalculationPage), typeof(CreateCalculationPage));
            Routing.RegisterRoute(nameof(CalculationPage), typeof(CalculationPage));
        }
    }
}