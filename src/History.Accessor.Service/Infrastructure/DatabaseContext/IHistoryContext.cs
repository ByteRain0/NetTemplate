using System.Threading;
using System.Threading.Tasks;
using History.Accessor.Service.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace History.Accessor.Service.Infrastructure.DatabaseContext;

public interface IHistoryContext
{
    DbSet<EventDataModel> Events { get; set; }
        
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}