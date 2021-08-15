using System.Collections.Generic;
using System.Threading.Tasks;
using Instruments.Searching.Engine.Contracts.Contracts.Contracts;
using Utilities.MediatRPipeline.ExceptionHandling;

namespace Instruments.Searching.Engine.Contracts.Contracts.ServiceLevelContracts
{
    public interface ISearchEngine
    {
        Task<Response<IList<ProductDto>>> SearchCheapestProductsInRegion(string region, string type);
    }
}