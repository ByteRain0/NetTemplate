using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using History.Accessor.Contracts.Contracts;
using MediatR;

namespace History.Accessor.Service.Service.Commands.RecordEvent
{
    public class RecordEvent : EventDto, IRequest<Response>, IRetryMarker
    {
    }
}