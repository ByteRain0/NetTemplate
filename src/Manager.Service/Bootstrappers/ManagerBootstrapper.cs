using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Manager.Service.Service.History.Commands.RecordEvent;
using Microsoft.Extensions.DependencyInjection;
using Voyager;

namespace Manager.Service.Bootstrappers
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