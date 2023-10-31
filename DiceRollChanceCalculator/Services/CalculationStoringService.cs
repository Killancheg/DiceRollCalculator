using DiceRollChanceCalculator.Models;
using Newtonsoft.Json;

namespace DiceRollChanceCalculator.Services;

public class CalculationStoringService
{
    private readonly string jsonFilesDirectory;

    public CalculationStoringService(string directoryPath)
    {
        jsonFilesDirectory = directoryPath;
        Directory.CreateDirectory(jsonFilesDirectory);
    }

    public async Task SaveModelToJsonAsync(CalculationModel model, string fileName)
    {
        var filePath = Path.Combine(jsonFilesDirectory, fileName);

        var json = JsonConvert.SerializeObject(model, Formatting.Indented);
        using StreamWriter writer = new StreamWriter(filePath);
        await writer.WriteAsync(json);
    }

    public async Task<CalculationModel> OpenModelFromJsonAsync(string fileName)
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

    public async Task<List<CalculationModel>> GetAllModelsFromDirectoryAsync()
    {
        var models = new List<CalculationModel>();

        foreach (var filePath in Directory.GetFiles(jsonFilesDirectory, "*.json"))
        {
            if (File.Exists(filePath))
            {
                string json = await File.ReadAllTextAsync(filePath);
                var model = JsonConvert.DeserializeObject<CalculationModel>(json);
                models.Add(model);
            }
        }

        return models;
    }
}
