namespace newscleanerconsole.Services;

public class FileLogger : IFileLogger
{
    private readonly string _path;
    public FileLogger(string path) => _path = path;

    public void Log(string message)
    {
        File.AppendAllText(_path, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n");
    }
}