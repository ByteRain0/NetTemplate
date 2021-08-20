using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PoC.Searching.Engine.Contracts.ServiceLevelContracts;
using PoC.Searching.Engine.Service.Service;

namespace PoC.Searching.Engine.Host.Bootstrappers
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