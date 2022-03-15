using BlobStorage.Accessor.Host.Bootstrappers;
using ExecutionPipeline.Bootstrapper;
using History.Accessor.Host.Bootstrappers;
using Localization.Accessor.Infrastructure.Bootstrapper;
using Manager.Host.Bootstrappers;
using Manager.Service.Bootstrappers;
using MessageDispatcher.Host.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Session.Accessor.Service.Host.Bootstrappers;
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
        services.AddEventHistory(Configuration);
        services.AddExecutionPipeline();
        services.AddSessionAccessors();
        services.AddMessageDispatcher(Configuration);
        services.AddBlobStorage(Configuration);
        services.AddCustomLocalization(Configuration);
        services.AddManagerServices();
        services.AddSwagger();
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
}