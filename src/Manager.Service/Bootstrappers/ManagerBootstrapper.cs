using System.Collections.Generic;
using System.Reflection;
using ExecutionPipeline.Bootstrappers;
using Manager.Service.Services.History.Commands.RecordEvent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Voyager;

namespace Manager.Service.Bootstrappers;

public class ManagerBootstrapper : IBootstrapper
{
    public void BootstrapServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddVoyager(c => { c.AddAssemblyWith<RecordEvent>(); });
        services.AddAutoMapper(c =>
        {
            c.AddMaps(new List<Assembly>() {typeof(RecordEvent).Assembly});
        });
    }
}