using ExecutionPipeline.Bootstrappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Manager.Host.Bootstrappers;

public class SwaggerBootstrapper : IBootstrapper
{
    public void BootstrapServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApi", Version = "v1"});
        });
    }
}

public static class SwaggerMiddleware
{
    public static void AddSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
    }
}