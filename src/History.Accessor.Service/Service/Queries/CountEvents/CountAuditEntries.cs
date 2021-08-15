using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;
using Utilities.MediatRPipeline.Retry;

namespace HistoryAccessorService.Service.Queries.CountEvents
{
    public class CountAuditEntries : IRequest<Response<int>>, IRetryMarker
    {
        //
    }
}