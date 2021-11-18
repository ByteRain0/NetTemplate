using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.Commands;
using History.Accessor.Contracts.DTO_s;
using History.Accessor.Contracts.Queries;

namespace History.Accessor.Contracts
{
    /// <summary>
    /// Service methods are all async.
    /// </summary>
    public interface IHistoryAccessor
    {
        Task<Response<EventOverviewDto>> GetEvents(GetEventsQuery query, CancellationToken cancellationToken);

        Task<Response> RecordEvent(RecordEvent request, CancellationToken cancellationToken);

        Task<Response<int>> CountEvents(CountEventEntries request, CancellationToken cancellationToken);
    }
}