using HtmlAgilityPack;
using newscleanerconsole.Models;
using System.Text.Json;

namespace newscleanerconsole.Services;

public class NewsParser : INewsParser
{
    private const string BaseUrl = "https://brokennews.net";
    private readonly IFileLogger _logger;

    public NewsParser(
        IFileLogger logger )
    {
        _logger = logger;
    }

    public List<NewsItem> Parse(string htmlPath)
    {
        var doc = new HtmlDocument();
        doc.Load(htmlPath);
        var newsNodes = doc.DocumentNode.SelectNodes("//li[contains(@class, 'news-item')]");
        var newsList = new List<NewsItem>();

        if (newsNodes is null) return newsList;

        foreach (var node in newsNodes)
        {
            try
            {
                // Пропуск баннеров, футеров итд
                if (node.InnerHtml.Contains("ad-banner") || node.InnerHtml.Contains("footer"))
                {
                    _logger.Log("Skipped junk block.");
                    continue;
                }

                var timeNode = node.SelectSingleNode(".//time[@datetime]");
                var titleNode = node.SelectSingleNode(".//a");

                if (timeNode == null || titleNode == null)
                {
                    _logger.Log("Skipped block: missing <time> or <a>.");
                    continue;
                }

                string date = timeNode.GetAttributeValue("datetime", "").Trim();
                string url = titleNode.GetAttributeValue("href", "").Trim();
                string title = TextCleaner.CleanText(titleNode.InnerText);

                if (string.IsNullOrEmpty(date) || string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(title))
                {
                    _logger.Log($"Skipped block: invalid data (Date: '{date}', Url: '{url}', Title: '{title}').");
                    continue;
                }

                newsList.Add(new NewsItem
                {
                    Date = date,
                    Url = url.StartsWith("http") ? url : BaseUrl + url,
                    Title = title
                });
            }
            catch (Exception ex)
            {
                _logger.Log("Error parsing block: " + ex.Message);
            }
        }

        return newsList;
    }
}