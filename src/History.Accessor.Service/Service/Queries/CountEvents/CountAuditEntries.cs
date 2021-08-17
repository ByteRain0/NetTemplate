using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;

namespace History.Accessor.Service.Service.Queries.CountEvents
{
    public class CountAuditEntries : IRequest<Response<int>>, IRetryMarker
    {
        //
    }
}