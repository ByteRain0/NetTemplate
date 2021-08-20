using System.Collections.Generic;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using PoC.Searching.Engine.Contracts.Contracts;

namespace PoC.Searching.Engine.Contracts.ServiceLevelContracts
{
    public interface ISearchEngine
    {
        Task<Response<IList<ProductDto>>> SearchCheapestProductsInRegion(string region, string type);
    }
}