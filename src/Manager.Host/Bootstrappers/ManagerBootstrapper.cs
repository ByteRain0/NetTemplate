using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Orchestra.Manager.Service.History.Commands.RecordEvent;
using Voyager;

namespace CloudOrchestra.Bootstrappers
{
    public static class OrchestraManagerBootstrapper
    {
        public static void ConfigureManagerServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddVoyager(c => { c.AddAssemblyWith<RecordEvent>(); });
            services.AddAutoMapper(c =>
            {
                c.AddMaps(new List<Assembly>() {typeof(OrchestraManagerBootstrapper).Assembly});
            });
        }
    }
}