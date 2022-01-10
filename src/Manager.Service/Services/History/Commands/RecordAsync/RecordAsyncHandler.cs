using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using MessageDispatcher.Contracts;

namespace Manager.Service.Services.History.Commands.RecordAsync;

public class RecordAsyncHandler : IRequestHandler<RecordAsync, Response>
{
    private readonly IMediator _mediator;

    public RecordAsyncHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Response> Handle(RecordAsync request, CancellationToken cancellationToken)
    {
        return _mediator.Enqueue(Guid.NewGuid().ToString(), new RecordEvent.RecordEvent()
        {
            Message = request.Message,
            EntityPrimaryKey = request.EntityPrimaryKey,
            EntityType = request.EntityType,
            EventName = request.EventName,
            UserId = request.UserId,
            UserName = request.UserName
        });
    }
}