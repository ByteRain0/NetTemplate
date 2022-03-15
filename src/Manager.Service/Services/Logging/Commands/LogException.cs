using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Voyager.Api;

namespace Manager.Service.Services.Logging.Commands;

[VoyagerRoute(HttpMethod.Post, "Logs/Exception")]
public class LogException : IRequest<Response>
{
    public class LogExceptionHandler : IRequestHandler<LogException, Response>
    {
        private readonly ILogger<LogExceptionHandler> _logger;

        public LogExceptionHandler(ILogger<LogExceptionHandler> logger)
        {
            _logger = logger;
        }

        public Task<Response> Handle(LogException request, CancellationToken cancellationToken)
        {
            try
            {
                throw new Exception("An exception occured. Some error message.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return Task.FromResult(Response.Fail(e.Message));
            }
        }
    }
}