using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ExecutionPipeline.Bootstrappers;
using History.Accessor.Host.Bootstrappers;
using Localization.Accessor.Infrastructure.Bootstrapper;
using Manager.Host.Bootstrappers;
using MessageDispatcher.Host.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Voyager;

namespace Manager.Host;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.RunBootstrapping(GetAssemblies(), Configuration);
        services.AddMessageDispatcher(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.ApplyEventHistoryMigrations();
            
        app.AddSwagger();
            
        app.UseVoyagerExceptionHandler();
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();
        app.UseSession();
        app.UseLocalization();

        app.UseMessageDispatcher();
            
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapVoyager();
        });
            
        app.UseHealthChecks();
    }

    public List<Assembly> GetAssemblies()
    {
        var referencedAssemblyNames = DependencyContext.Default.GetDefaultAssemblyNames();
        var assemblies = referencedAssemblyNames.Select(Assembly.Load).ToList();
        return assemblies;
    }
}