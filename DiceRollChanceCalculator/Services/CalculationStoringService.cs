using DiceRollChanceCalculator.Models;
using Newtonsoft.Json;

namespace DiceRollChanceCalculator.Services;

public class CalculationStoringService
{
    private readonly string jsonFilesDirectory;

    private List<CalculationModel> Calculations { get; set; }

    public CalculationStoringService()
    {
        jsonFilesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Calculations");
        Directory.CreateDirectory(jsonFilesDirectory);
    }

    public async Task<List<CalculationModel>> GetAllCalcutionsAync()
    {
        if (Calculations == null)
        {
            await GetAllCalculationsFromDirectoryAsync();
        }

        return Calculations;
    }

    public async Task SaveCalcutaionAsync(CalculationModel calculation)
    {
        if (calculation == null)
        {
            return;
        }
        
        Calculations.Add(calculation);

        await SaveCalculationToJsonAsync(calculation);
    }

    public void DeleteCalculation(CalculationModel calculation)
    {
        if (!Calculations.Any() || calculation==null) 
        {
            return;
        }

        Calculations.Remove(calculation);

        DeleteCalculationJsonFileAsync(GetFilenameFromCalculation(calculation));
    }

    private async Task SaveCalculationToJsonAsync(CalculationModel calculation)
    {
        var filename = GetFilenameFromCalculation(calculation);
        var filePath = Path.Combine(jsonFilesDirectory, filename);

        var json = JsonConvert.SerializeObject(calculation, Formatting.Indented);
        using StreamWriter writer = new StreamWriter(filePath);
        await writer.WriteAsync(json);
    }

    private async Task<CalculationModel> OpenCalculationFromJsonAsync(string fileName)
    {
        var filePath = Path.Combine(jsonFilesDirectory, fileName);

        if (File.Exists(filePath))
        {
            using StreamReader reader = new StreamReader(filePath);
            var json = await reader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<CalculationModel>(json);
        }
        else
        {
            return null;
        }
    }

    private void DeleteCalculationJsonFileAsync(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return;
        }

        var filePath = Path.Combine(jsonFilesDirectory, fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);

            // Also remove the calculation from the in-memory list if it exists
            var calculationToRemove = Calculations.FirstOrDefault(calculation => calculation.Name == fileName);
            if (calculationToRemove != null)
            {
                Calculations.Remove(calculationToRemove);
            }
        }
    }

    private async Task GetAllCalculationsFromDirectoryAsync()
    {
        Calculations = new List<CalculationModel>();

        foreach (var filePath in Directory.GetFiles(jsonFilesDirectory, "*.json"))
        {
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                var model = JsonConvert.DeserializeObject<CalculationModel>(json);
                Calculations.Add(model);
            }
        }
    }

    private string GetFilenameFromCalculation(CalculationModel calculation)
    {
        return $"{calculation.Name} {DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}";
    }
}
