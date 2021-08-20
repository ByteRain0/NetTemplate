using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;

namespace ExecutionPipeline.MediatRPipeline.Retry
{
    public class RequestRetryMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        private readonly RetryMiddlewareOptions _config;

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
            
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(retryCount: _config.OperationRetryCount, sleepDurationProvider: retryAttempt =>
                    {
                        var timeToWait = TimeSpan.FromSeconds(retryAttempt * _config.OperationIncrementalCount);
                        _logger.LogTrace(
                            $"Execution delay of request '{JsonConvert.SerializeObject(request)}' for '{timeToWait.TotalSeconds}' seconds...");
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
}