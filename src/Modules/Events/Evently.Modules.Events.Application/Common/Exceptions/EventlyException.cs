﻿namespace Evently.Modules.Events.Application.Common.Exceptions;

public sealed class EventlyException(string requestName, Error? error = default, Exception? innerException = default)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;

    public Error? Error { get; } = error;
}
