using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace History.Accessor.Service.Service.Queries.CountEvents
{
    internal class CountAuditEntriesHandler : IRequestHandler<CountAuditEntries, Response<int>>
    {
        private readonly IHistoryContext _context;

        public CountAuditEntriesHandler(IHistoryContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> Handle(CountAuditEntries request, CancellationToken cancellationToken)
        {
            return Response.Ok(await _context.Events.CountAsync(CancellationToken.None));
        }
    }
}