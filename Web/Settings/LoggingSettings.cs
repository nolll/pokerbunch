using Microsoft.Extensions.Logging;

namespace Web.Settings;

public class LoggingSettings
{
    public LoggingLogLevelSettings LogLevel { get; set; }
    public LoggingLoggersSettings Loggers { get; set; }
}