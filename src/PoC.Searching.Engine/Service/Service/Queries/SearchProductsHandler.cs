using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Instruments.Searching.Engine.Contracts.Contracts.Contracts;
using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;

namespace Instruments.Searching.Engine.Contracts.Service.Service.Queries
{
    public class SearchProductsHandler : IRequestHandler<SearchProducts, Response<IList<ProductDto>>>
    {
        public Task<Response<IList<ProductDto>>> Handle(SearchProducts request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Response.Ok((IList<ProductDto>) new List<ProductDto>()
            {
                new ProductDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = 100,
                    Type = request.Type,
                    VendorName = "MusicShop"
                },
                new ProductDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = 200,
                    Type = request.Type,
                    VendorName = "MusicShop"
                },
                new ProductDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    Price = 300,
                    Type = request.Type,
                    VendorName = "MusicShop"
                }
            }));
        }
    }
}