using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Voyager.Api;

namespace Manager.Service.Services.Logging.Commands;

[VoyagerRoute(HttpMethod.Post,"Logs/UnstructuredData")]
public class LogUnstructuredData : IRequest<Response>
{
    public class LogUnstructuredDataHandler : IRequestHandler<LogUnstructuredData, Response>
    {
        private readonly ILogger<LogUnstructuredDataHandler> _logger;

        public LogUnstructuredDataHandler(ILogger<LogUnstructuredDataHandler> logger)
        {
            _logger = logger;
        }

        public Task<Response> Handle(LogUnstructuredData request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Hello this is a an unstructured message");
            return Task.FromResult(Response.Ok());
        }
    }
}