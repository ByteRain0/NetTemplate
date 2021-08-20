using System.Collections.Generic;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;
using PoC.Searching.Engine.Contracts.Contracts;

namespace PoC.Searching.Engine.Service.Service.Queries
{
    public class SearchProducts : IRequest<Response<IList<ProductDto>>>, IRetryMarker
    {
        public string Region { get; set; }

        public string Type { get; set; }
    }
}