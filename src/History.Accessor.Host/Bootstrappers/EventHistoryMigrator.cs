using History.Accessor.Service.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace History.Accessor.Host.Bootstrappers;

public static class EventHistoryMigrator
{
    public static void ApplyEventHistoryMigrations(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            using (var context = serviceScope.ServiceProvider.GetService<HistoryContext>())
            {
                context?.Database.Migrate();
            }
        }
    }
}