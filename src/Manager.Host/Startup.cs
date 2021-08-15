using CloudOrchestra.Bootstrappers;
using HistoryAccessorHost.Bootstrappers;
using Instruments.Searching.Engine.Contracts.Host.Bootstrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SessionAccessor.Host.Bootstrappers;
using Utilities.Bootstrapper;
using Voyager;

namespace CloudOrchestra
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureSwagger();
            
            app.UseVoyagerExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapVoyager();
            });
            
            app.EnableHealthChecks();
        }
    }
}