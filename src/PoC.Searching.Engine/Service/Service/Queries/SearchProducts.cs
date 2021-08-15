using System.Collections.Generic;
using Instruments.Searching.Engine.Contracts.Contracts.Contracts;
using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;
using Utilities.MediatRPipeline.Retry;

namespace Instruments.Searching.Engine.Contracts.Service.Service.Queries
{
    public class SearchProducts : IRequest<Response<IList<ProductDto>>>, IRetryMarker
    {
        public string Region { get; set; }

        public string Type { get; set; }
    }
}