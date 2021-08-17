using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.Contracts;

namespace History.Accessor.Contracts.ServiceLevelContracts
{
    /// <summary>
    /// Service methods are all async.
    /// </summary>
    public interface IHistoryAccessor
    {
        Task<Response<EventOverviewDto>> GetEvents(EventOverviewFilter filter, CancellationToken cancellationToken);

        Task<Response> RecordEvent(EventDto model, CancellationToken cancellationToken);

        Task<Response<int>> CountEntries(CancellationToken cancellationToken);
    }
}