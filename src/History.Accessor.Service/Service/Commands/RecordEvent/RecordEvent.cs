using HistoryAccessor.Contracts;
using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;
using Utilities.MediatRPipeline.Retry;

namespace HistoryAccessorService.Service.Commands.RecordEvent
{
    public class RecordEvent : EventDto, IRequest<Response>, IRetryMarker
    {
    }
}