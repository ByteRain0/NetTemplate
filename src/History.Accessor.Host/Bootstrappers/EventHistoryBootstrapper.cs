using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using History.Accessor.Contracts.ServiceLevelContracts;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using History.Accessor.Service.Infrastructure.HealthChecks;
using History.Accessor.Service.Service;
using History.Accessor.Service.Service.Commands.RecordEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace History.Accessor.Host.Bootstrappers
{
    public static class EventHistoryBootstrapper
    {
        public static void ConfigureEventHistory(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IHistoryContext, HistoryContext>();
            services.AddTransient<IHistoryAccessor, HistoryAccessor>();
            services.AddMediatR(typeof(HistoryAccessor));
            
            FluentValidation.ServiceCollectionExtensions.AddValidatorsFromAssemblies(services, 
                new List<Assembly>(){typeof(RecordEvent).Assembly});
            
            services.AddAutoMapper(c =>
            {
                c.AddMaps(new List<Assembly>() {typeof(RecordEvent).Assembly});
            });
            
            services.AddDbContext<HistoryContext>(
                opts => opts.UseNpgsql(config["DatabaseConnectionString"])
            );
            
            var hcBuilder = services.AddHealthChecks();
            
            hcBuilder.AddCheck<HistoryAccessorHealthCheck>(
                "history_accessor_health_check",
                failureStatus: HealthStatus.Degraded,
                tags: new[] { "HistoryAccessor" });
        }
    }
}