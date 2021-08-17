using System.Collections.Generic;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using PoC.Searching.Engine.Contracts.Contracts;
using PoC.Searching.Engine.Contracts.ServiceLevelContracts;
using PoC.Searching.Engine.Service.Service.Queries;

namespace PoC.Searching.Engine.Service.Service
{
    /// <summary>
    /// Our case this is going to be a stub.
    /// Examples: an external API, a complex local service, an executable on the host machine etc.
    /// </summary>
    public class SearchServiceStub : ISearchEngine
    {
        private readonly IMediator _mediator;

        public SearchServiceStub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Response<IList<ProductDto>>> SearchCheapestProductsInRegion(string region, string type)
        {
            var operation = _mediator.Send(new SearchProducts()
            {
                Region = region,
                Type = type
            });

            return operation;
        }
    }
}