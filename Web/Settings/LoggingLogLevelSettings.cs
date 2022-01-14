using Microsoft.Extensions.Logging;

namespace Web.Settings;

public class LoggingLogLevelSettings
{
    public LogLevel Default { get; set; } = LogLevel.Error;
}