using History.Accessor.Service.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace History.Accessor.Service.Infrastructure.DatabaseContext;

/// <summary>
///  In order to add migration run : dotnet ef migrations add InitialMigration -s ../Manager.Service.Host/
/// </summary>
public class HistoryContext : DbContext, IHistoryContext
{
    public HistoryContext(DbContextOptions<HistoryContext> options) : base(options)
    {
    }
        
    public DbSet<EventDataModel> Events { get; set; }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HistoryContext).Assembly);
    }
}