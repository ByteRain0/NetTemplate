using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Service.PoC.Queries
{
    [VoyagerRoute(HttpMethod.Get,"api/GetPropFromQuery")]
    public class FromQueryExample : IRequest<Response>
    {
        [Voyager.Api.FromQuery("PropertyFromQuery")]
        public string PropertyFromQuery { get; set; }
        
        public class FromQueryExampleHandler : IRequestHandler<FromQueryExample,Response>
        {
            public Task<Response> Handle(FromQueryExample request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Value from the query string" + request.PropertyFromQuery);
                return Task.FromResult(Response.Ok());
            }
        }
        
    }
}