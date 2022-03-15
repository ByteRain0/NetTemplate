using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Voyager.Api;

namespace Manager.Service.Services.Logging.Commands;

[VoyagerRoute(HttpMethod.Post,"Logs/ArgumentPass")]
public class LogWithParametersPassing : IRequest<Response>
{
    public class LogWithParametersPassingHandler : IRequestHandler<LogWithParametersPassing, Response>
    {
        private readonly ILogger<LogWithParametersPassingHandler> _logger;

        public LogWithParametersPassingHandler(ILogger<LogWithParametersPassingHandler> logger)
        {
            _logger = logger;
        }

        public Task<Response> Handle(LogWithParametersPassing request, CancellationToken cancellationToken)
        {
            var testObject = new TestObject()
            {
                SomeProperty = "Some default value for the property."
            };
            
            _logger.LogInformation($"Test object's Some property is : '{testObject.SomeProperty}'.");
            return Task.FromResult(Response.Ok());
        }
    }
    
    public class TestObject
    {
        public string SomeProperty { get; set; }

    }
}