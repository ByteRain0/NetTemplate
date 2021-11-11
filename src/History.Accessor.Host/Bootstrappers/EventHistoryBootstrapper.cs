using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using History.Accessor.Contracts;
using History.Accessor.Contracts.Commands;
using History.Accessor.Host.HealthChecks;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using History.Accessor.Service.Service;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace History.Accessor.Host.Bootstrappers
{
    public static class EventHistoryBootstrapper
    {
        public static void AddEventHistory(this IServiceCollection services, IConfiguration config)
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

        public static void ApplyEventHistoryMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<HistoryContext>())
                {
                    context?.Database.Migrate();
                }
            }
        }
    }
}