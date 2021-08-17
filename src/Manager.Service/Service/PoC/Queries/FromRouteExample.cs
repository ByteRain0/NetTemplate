using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Service.PoC.Queries
{
    [VoyagerRoute(HttpMethod.Post, "api/GetPropertyFromRoute/{property}")]
    public class FromRouteExample : IRequest<Response>
    {
        [Voyager.Api.FromRoute]
        public string Property { get; set; }
        
        public class FromRouteExampleHandler : IRequestHandler<FromRouteExample, Response>
        {
            public Task<Response> Handle(FromRouteExample request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Value from route : " + request.Property);
                return Task.FromResult(Response.Ok());
            }
        }
    }
}