using System.Collections.Generic;
using System.Reflection;
using ExecutionPipeline.Bootstrappers;
using History.Accessor.Contracts;
using History.Accessor.Host.HealthChecks;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using History.Accessor.Service.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace History.Accessor.Host.Bootstrappers;

public class EventHistoryBootstrapper : IBootstrapper
{
    public void BootstrapServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IHistoryContext, HistoryContext>();
        services.AddTransient<IHistoryAccessor, HistoryAccessor>();
        services.AddMediatR(typeof(HistoryAccessor));
            
        FluentValidation.ServiceCollectionExtensions.AddValidatorsFromAssemblies(services, 
            new List<Assembly>()
            {
                typeof(HistoryContext).Assembly
            });
            
        services.AddAutoMapper(c =>
        {
            c.AddMaps(new List<Assembly>() {typeof(HistoryContext).Assembly});
        });
            
        services.AddDbContext<HistoryContext>(
            opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
            
        var hcBuilder = services.AddHealthChecks();
            
        hcBuilder.AddCheck<HistoryAccessorHealthCheck>(
            "history_accessor_health_check",
            failureStatus: HealthStatus.Degraded,
            tags: new[] { "HistoryAccessor" });
    }
}