using DogShelter.Interfaces;

namespace DogShelter.Mocks;

public class LoggerServiceMock : ILoggerService
{
    private List<LogEntry> _logs = new List<LogEntry>();
    private int _infoCount = 0;
    private int _errorCount = 0;
    public LogLevel MinLogLevel { get; set; } = LogLevel.Info;
    public bool ThrowOnError { get; set; } = false;

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public class LogEntry
    {
        public LogLevel Level { get; set; }
        public string Message { get; set; } = "";
        public DateTime Timestamp { get; set; }
    }

    public void LogInfo(string message)
    {
        _infoCount++;
        if (MinLogLevel <= LogLevel.Info)
        {
            Console.WriteLine($"📝 MOCK LOG INFO #{_infoCount}: {message}");
            _logs.Add(new LogEntry { 
                Level = LogLevel.Info, 
                Message = message, 
                Timestamp = DateTime.Now 
            });
        }
    }

    public void LogError(string message)
    {
        _errorCount++;
        Console.WriteLine($"❌ MOCK LOG ERROR #{_errorCount}: {message}");
        _logs.Add(new LogEntry { 
            Level = LogLevel.Error, 
            Message = message, 
            Timestamp = DateTime.Now 
        });
        if (ThrowOnError)
        {
            throw new Exception($"Logger error: {message}");
        }
    }

    public int GetInfoCount() => _infoCount;
    public int GetErrorCount() => _errorCount;
    public int GetTotalLogCount() => _logs.Count;
    public List<LogEntry> GetLogs() => _logs;
    public List<LogEntry> GetLogsByLevel(LogLevel level) => _logs.Where(l => l.Level == level).ToList();
    public void ResetMock()
    {
        _logs.Clear();
        _infoCount = 0;
        _errorCount = 0;
        MinLogLevel = LogLevel.Info;
        ThrowOnError = false;
    }
    public LogEntry? GetLastLog() => _logs.LastOrDefault();
    public bool ContainsMessage(string substring) => _logs.Any(l => l.Message.Contains(substring));
}

