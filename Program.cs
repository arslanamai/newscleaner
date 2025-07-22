using System.Text.Json;
using newscleanerconsole.Models;
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
        
        if (File.Exists(logPath))
            File.Delete(logPath);

        IFileLogger logger = new FileLogger(logPath);
        INewsParser parser = new NewsParser(logger);

        try
        {
            List<NewsItem> newsList = parser.Parse(htmlPath);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(newsList, options);

            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Обработано: {newsList.Count} новостей.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            logger.Log($"Exception: {ex}");
        }
    }
}
