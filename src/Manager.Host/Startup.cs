using ExecutionPipeline.Bootstrapper;
using History.Accessor.Host.Bootstrappers;
using Manager.Host.Bootstrappers;
using Manager.Service.Bootstrappers;
using MessageDispatcher.Host.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PoC.Searching.Engine.Host.Bootstrappers;
using Session.Accessor.Service.Host.Bootstrappers;
using Voyager;

namespace Manager.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureSwagger();
            services.ConfigureEventHistory(Configuration);
            services.ConfigureExecutionPipeline();
            services.ConfigureManagerServices();
            services.ConfigureSearchingEngine();
            services.ConfigureSessionAccessors();
            services.ConfigureMessageDispatcher(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplyEventHistoryMigrations();
            app.ConfigureSwagger();
            
            app.UseVoyagerExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureMessageDispatcher();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapVoyager();
            });
            
            app.EnableHealthChecks();
        }
    }
}