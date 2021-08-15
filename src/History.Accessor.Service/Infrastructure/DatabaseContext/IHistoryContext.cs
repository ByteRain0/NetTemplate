using System.Threading;
using System.Threading.Tasks;
using HistoryAccessorService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryAccessorService.Infrastructure.DatabaseContext
{
    public interface IHistoryContext
    {
        DbSet<EventDataModel> Events { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}