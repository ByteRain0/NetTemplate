using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using History.Accessor.Contracts.DTO_s;
using MediatR;

namespace History.Accessor.Contracts.Commands.RecordEvent;

public class RecordEventCommand : EventDTO, IRequest<Response>, IRetryMarker
{
}