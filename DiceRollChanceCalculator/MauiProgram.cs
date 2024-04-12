using DiceRollChanceCalculator.Services;
using DiceRollChanceCalculator.ViewModels;
using DiceRollChanceCalculator.Views;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace DiceRollChanceCalculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            //Pages
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<CreateCalculationViewModel>();
            builder.Services.AddTransient<CreateCalculationPage>();
            builder.Services.AddScoped<CalculationViewModel>();
            builder.Services.AddScoped<CalculationPage>();
            //Services
            builder.Services.AddSingleton<CalculationStoringService>();
            builder.Services.AddTransient<DiceRollsCalculationService>();
            builder.Services.AddTransient<DiceRollsSimulationService>();
            builder.Services.AddScoped<IDiceRollsServiceProvider, DiceRollsServiceProvider>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}