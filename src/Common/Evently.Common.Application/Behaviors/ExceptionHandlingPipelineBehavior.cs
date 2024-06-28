namespace Evently.Common.Application.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return next();
        }
        catch (Exception exception)
        {
            Logging.Log.UnhandledExceptionFor(logger, typeof(TRequest).Name);

            throw new EventlyException(typeof(TRequest).Name, innerException: exception);
        }
    }
}
