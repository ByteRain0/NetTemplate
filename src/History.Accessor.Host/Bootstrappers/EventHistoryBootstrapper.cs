using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using HistoryAccessor.ServiceLevelContracts;
using HistoryAccessorService.Infrastructure.DatabaseContext;
using HistoryAccessorService.Service.Commands.RecordEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HistoryAccessorHost.Bootstrappers
{
    public static class EventHistoryBootstrapper
    {
        public static void ConfigureEventHistory(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IHistoryContext, HistoryContext>();
            services.AddTransient<IHistoryAccessor, HistoryAccessorService.Service.HistoryAccessor>();
            services.AddMediatR(typeof(HistoryAccessorService.Service.HistoryAccessor));
            
            FluentValidation.ServiceCollectionExtensions.AddValidatorsFromAssemblies(services, 
                new List<Assembly>(){typeof(RecordEvent).Assembly});
            
            services.AddAutoMapper(c =>
            {
                c.AddMaps(new List<Assembly>() {typeof(RecordEvent).Assembly});
            });
            
            services.AddDbContext<HistoryContext>(
                opts => opts.UseNpgsql(config["DatabaseConnectionString"])
            );
        }
    }
}