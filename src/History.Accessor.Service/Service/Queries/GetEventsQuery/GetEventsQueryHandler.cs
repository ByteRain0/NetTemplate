using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace History.Accessor.Service.Service.Queries.GetEventsQuery
{
    internal class GetEventsQueryHandler : IRequestHandler<Contracts.Queries.GetEventsQuery, Response<EventOverviewDto>>
    {
        private readonly IHistoryContext _context;

        public GetEventsQueryHandler(IHistoryContext context)
        {
            _context = context;
        }

        public async Task<Response<EventOverviewDto>> Handle(Contracts.Queries.GetEventsQuery request, CancellationToken cancellationToken)
        {
            var entriesQuery = _context.Events
                .OrderByDescending(x => x.DateTime)
                .Where(request.ActionFilter())
                .Where(request.EntityFilter())
                .Where(request.UserIdFilter())
                .Where(request.EntityPkFilterWithAndContainsMessage())
                .Where(request.EntityPkFilterWithOrElseOptionalMessage());

            var entriesCount = await entriesQuery.CountAsync(cancellationToken);

            var entriesList = await entriesQuery
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(x => new EventDto()
                {
                    Id = x.Id,
                    Message = x.Message,
                    EventName = x.EventName,
                    DateTime = x.DateTime,
                    EntityType = string.IsNullOrWhiteSpace(x.EntityType) ? x.EntityType : string.Empty,
                    EntityPrimaryKey = string.IsNullOrWhiteSpace(x.EntityPrimaryKey)
                        ? x.EntityPrimaryKey
                        : string.Empty,
                    UserName = string.IsNullOrEmpty(x.UserName) ? x.UserName : string.Empty,
                    UserId = x.UserId
                })
                .ToListAsync(cancellationToken: CancellationToken.None);

            return Response.Ok(new EventOverviewDto()
            {
                Events = entriesList,
                Count = entriesCount
            });
        }
        
        
        
        
    }
}