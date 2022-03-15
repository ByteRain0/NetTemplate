using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.Logging.Commands;

[VoyagerRoute(HttpMethod.Post,"Logs/LongRunning")]
public class LogLongRunningRequest : IRequest<Response>
{
    internal class LogLongRunningRequestHandler : IRequestHandler<LogLongRunningRequest, Response>
    {
        public Task<Response> Handle(LogLongRunningRequest request, CancellationToken cancellationToken)
        {
            Thread.Sleep(10000);
            return Task.FromResult(Response.Ok());
        }
    }
}