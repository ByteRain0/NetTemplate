using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;
using Voyager.Api;

namespace Orchestra.Manager.Service.PoC.Queries
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