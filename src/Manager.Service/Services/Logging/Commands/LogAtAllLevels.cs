using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Voyager.Api;

namespace Manager.Service.Services.Logging.Commands;

[VoyagerRoute(HttpMethod.Post,"Logs/LogAtAllLevels")]
public class LogAtAllLevels : IRequest<Response>
{
    internal class LogAtAllLevelsHandler : IRequestHandler<LogAtAllLevels, Response>
    {
        private readonly ILogger<LogAtAllLevelsHandler> _logger;

        public LogAtAllLevelsHandler(ILogger<LogAtAllLevelsHandler> logger)
        {
            _logger = logger;
        }

        public Task<Response> Handle(LogAtAllLevels request, CancellationToken cancellationToken)
        {
            _logger.LogTrace("Detailed messages with sensitive app data");
            
            _logger.LogDebug("Useful for the development environment");
            
            _logger.LogInformation("General messages");
            
            _logger.LogWarning("Negative events that can be gracefully handled by application code");
            
            _logger.LogError("For exceptions and errors");
            
            _logger.LogCritical("For failures that may need immediate attention");
            
            return Task.FromResult(Response.Ok());
        }
    }
}