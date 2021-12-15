using System.Collections.Generic;
using System.Reflection;
using Manager.Service.Services.History.Commands.RecordEvent;
using Microsoft.Extensions.DependencyInjection;
using Voyager;

namespace Manager.Host.Bootstrappers
{
    public static class OrchestraManagerBootstrapper
    {
        public static void AddManagerServices(this IServiceCollection services)
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