using Instruments.Searching.Engine.Contracts.Contracts.ServiceLevelContracts;
using Instruments.Searching.Engine.Contracts.Service.Service;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Instruments.Searching.Engine.Contracts.Host.Bootstrappers
{
    public static class EngineBootstrapper
    {
        public static void ConfigureSearchingEngine(this IServiceCollection services)
        {
            services.AddMediatR(typeof(EngineBootstrapper).Assembly);
            services.AddTransient<ISearchEngine, SearchServiceStub>();
        }
    }
}