using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;

namespace History.Accessor.Contracts.Commands
{
    public class RecordEvent : EventDto, IRequest<Response>, IRetryMarker
    {
    }
}