using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.Queries.CountEventEntriesQuery;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace History.Accessor.Service.Service.Queries.CountEvents;

internal class CountAuditEntriesQueryHandler : IRequestHandler<CountEventEntriesQuery, Response<int>>
{
    private readonly IHistoryContext _context;

    public CountAuditEntriesQueryHandler(IHistoryContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(CountEventEntriesQuery request, CancellationToken cancellationToken)
    {
        return Response.Ok(await _context.Events.CountAsync(CancellationToken.None));
    }
}