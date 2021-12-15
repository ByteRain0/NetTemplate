using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExecutionPipeline.MediatRPipeline.Loggers;

public class RequestLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger _logger;

    public RequestLogger(ILogger<TRequest> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var name = typeof(TRequest).Name;

        var requestId = Guid.NewGuid();

        _logger.LogInformation("Executing request: '{RequestId}' '{RequestName}' '{RequestPayload}'", requestId, name,
            JsonConvert.SerializeObject(request));

        var response = await next();

        _logger.LogInformation("Executing request with id : '{RequestId}' has finished.",requestId);

        return response;
    }
}