using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;
using Voyager.Api;

namespace Orchestra.Manager.Service.PoC.Queries
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