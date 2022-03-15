using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExecutionPipeline.MediatRPipeline.Loggers;

public class RequestPerformanceMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Stopwatch _timer 
        = new Stopwatch();
        
    private readonly ILogger<TRequest> _logger;

    public RequestPerformanceMiddleware(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();
            
        var response = await next();
            
        _timer.Stop();
            
        if (_timer.ElapsedMilliseconds <= 5000) return response;
        
        var name = typeof(TRequest).Name;

        _logger.LogWarning("TemplateId : {TemplateId}. Long Running Request: RequestName : '{RequestName}' (Elapsed Time : '{ElapsedTime}' milliseconds). Payload : '{@RequestPayload}'.", 
            StructuredLogsTemplates.LongRunningRequestTemplate, name, _timer.ElapsedMilliseconds,  JsonConvert.SerializeObject(request));

        return response;
    }
}