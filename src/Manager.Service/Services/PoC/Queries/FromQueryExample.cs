using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Voyager.Api;

namespace Manager.Service.Services.PoC.Queries
{
    [VoyagerRoute(HttpMethod.Get,"api/GetPropFromQuery")]
    public class FromQueryExample : IRequest<Response>
    {
        [Voyager.Api.FromQuery("PropertyFromQuery")]
        public string PropertyFromQuery { get; set; }
        
        internal class FromQueryExampleHandler : IRequestHandler<FromQueryExample,Response>
        {
            public Task<Response> Handle(FromQueryExample request, CancellationToken cancellationToken)
            {
                Console.WriteLine("Value from the query string" + request.PropertyFromQuery);
                return Task.FromResult(Response.Ok());
            }
        }
        
    }
}