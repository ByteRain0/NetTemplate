using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CloudOrchestra.Bootstrappers
{
    public static class HealthChecksBootstrapper
    {
        public static void EnableHealthChecks(this IApplicationBuilder app)
        {
            app.UseRouting();
            
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
        }
    }
    
}