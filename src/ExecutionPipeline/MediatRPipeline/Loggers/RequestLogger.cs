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

        _logger.LogInformation("TemplateId : {TemplateId}. Executing request: '{RequestName}' with Payload : '{@RequestPayload}'", 
            StructuredLogsTemplates.StartExecutionTemplate, name, JsonConvert.SerializeObject(request));

        var response = await next();

        return response;
    }
}