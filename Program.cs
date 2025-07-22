using System.Text.Json;
using newscleanerconsole.Services;

namespace newscleanerconsole;

class Program
{
    static void Main(string[] args)
    {
        string baseDir = AppContext.BaseDirectory;

        string htmlPath = Path.Combine(baseDir, "corrupted-news.html");
        string outputPath = Path.Combine(baseDir, "clean-news.json");
        string logPath = Path.Combine(baseDir, "log.txt");

        if (File.Exists(logPath)) File.Delete(logPath);

        var parser = new NewsParser(logPath);
        var newsList = parser.Parse(htmlPath);

        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(newsList, options);

        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Done. Parsed: {newsList.Count} items.");
    }
}