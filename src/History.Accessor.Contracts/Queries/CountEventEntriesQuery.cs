using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;

namespace History.Accessor.Contracts.Queries
{
    public class CountEventEntriesQuery : IRequest<Response<int>>, IRetryMarker
    {
        //
    }
}