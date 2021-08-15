using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HistoryAccessorService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HistoryAccessorService.Infrastructure.DatabaseContext
{
    public class HistoryContextStub : IHistoryContext 
    {
        public HistoryContextStub()
        {
            Events.AddRange(new List<EventDataModel>()
            {
                new EventDataModel(message:"Seeded event 1",userId:"Vasilii.Oleinic",userName:"Vasilii Oleinic","Seeded event"),
                new EventDataModel(message:"Seeded event 2",userId:"Vasilii.Oleinic",userName:"Vasilii Oleinic","Seeded event"),
                new EventDataModel(message:"Seeded event 3",userId:"Vasilii.Oleinic",userName:"Vasilii Oleinic","Seeded event"),
                new EventDataModel(message:"Seeded event 4",userId:"Vasilii.Oleinic",userName:"Vasilii Oleinic","Seeded event"),
            });
        }
    
        public DbSet<EventDataModel> Events { get; set; }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}