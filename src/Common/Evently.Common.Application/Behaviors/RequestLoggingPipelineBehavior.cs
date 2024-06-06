namespace Evently.Common.Application.Behaviors;


public class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        string moduleName = GetModuleName(typeof(TRequest).FullName!);
        string requestName = typeof(TRequest).Name;

        using (LogContext.PushProperty("Module", moduleName))
        {
            Log.ProcessingRequest(logger, requestName);

            TResponse result = await next();

            if (result.IsSuccess)
            {
                Log.CompletedRequest(logger, requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    Log.CompletedRequestWithError(logger, requestName);
                }
            }

            return result;
        }
    }

    private static string GetModuleName(string requestName) => requestName.Split('.')[2];
}
