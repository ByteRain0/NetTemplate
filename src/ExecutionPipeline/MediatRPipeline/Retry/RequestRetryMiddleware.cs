using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.LoggingInfrastructure;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace ExecutionPipeline.MediatRPipeline.Retry;

public class RequestRetryMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;

    private RetryMiddlewareOptions _config;

    public RequestRetryMiddleware(ILogger<TRequest> logger, IOptions<RetryMiddlewareOptions> config)
    {
        _logger = logger;
        _config = config.Value;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        if (!((IList) typeof(TRequest).GetInterfaces()).Contains(typeof(IRetryMarker)))
        {
            return await next();
        }

        if (_config.CustomConfiguration is not null)
        {
            var customConfiguration = _config.CustomConfiguration.FirstOrDefault(x => x.Name == typeof(TRequest).Name);
            if (customConfiguration is not null)
            {
                _config.DefaultOperationIncrementalCount = customConfiguration.IncrementalCount;
                _config.DefaultOperationRetryCount = customConfiguration.RetryCount;
            }
        }

        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(retryCount: _config.DefaultOperationRetryCount, sleepDurationProvider: retryAttempt =>
                {
                    var timeToWait = TimeSpan.FromSeconds(retryAttempt * _config.DefaultOperationIncrementalCount);
                    _logger.LogTrace(
                        "TemplateId : {TemplateId}. Request : '{RequestName}'. Payload : '{Payload}'. is being delayed by : '{Timeout}' seconds...",
                        StructuredLogsTemplates.RequestWasRetried, typeof(TRequest).Name, JsonConvert.SerializeObject(request),timeToWait.TotalSeconds);
                    return timeToWait;
                },
                onRetry: (exception, pollyRetryCount, context) =>
                {
                    if (exception != null)
                    {
                        _logger.LogError(exception, exception.Message);
                    }
                });

        return await retryPolicy.ExecuteAsync(async () => await next());
    }
}