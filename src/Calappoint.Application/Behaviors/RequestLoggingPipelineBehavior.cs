using Calappoint.SharedKernel;
using MediatR;
using Serilog.Context;
using Microsoft.Extensions.Logging;

namespace Calappoint.Application.Behaviors;

// This pipeline behavior logs the request and response of the request it is handling.
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation("Handling request {RequestName}", requestName);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            logger.LogInformation("Request {RequestName} handled successfully", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Errors", result.Error, true))
            {
                logger.LogError("Request {RequestName} failed", requestName);
            }
        }

        return result;
    }
}
