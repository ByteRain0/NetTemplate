using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;

namespace History.Accessor.Contracts.Queries.CountEventEntriesQuery;

public class CountEventEntriesQuery : IRequest<Response<int>>, IRetryMarker
{
    //
}