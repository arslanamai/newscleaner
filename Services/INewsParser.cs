using newscleanerconsole.Models;

namespace newscleanerconsole.Services;

public interface INewsParser
{
    List<NewsItem> Parse(string htmlPath);
}