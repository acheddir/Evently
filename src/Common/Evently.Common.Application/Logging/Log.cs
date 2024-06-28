namespace Evently.Common.Application.Logging;

public static partial class Log
{
    [LoggerMessage(
        EventId = 10,
        Level = LogLevel.Information,
        Message = "Processing request {RequestName}")]
    public static partial void ProcessingRequest(ILogger logger, string requestName);

    [LoggerMessage(
        EventId = 11,
        Level = LogLevel.Information,
        Message = "Completed request {RequestName}")]
    public static partial void CompletedRequest(ILogger logger, string requestName);
    
    [LoggerMessage(
        EventId = 12,
        Level = LogLevel.Error,
        Message = "Completed request {RequestName} with error")]
    public static partial void CompletedRequestWithError(ILogger logger, string requestName);
    
    [LoggerMessage(
        EventId = 13,
        Level = LogLevel.Error,
        Message = "Unhandled exception for {RequestName}")]
    public static partial void UnhandledExceptionFor(ILogger logger, string requestName);
}
