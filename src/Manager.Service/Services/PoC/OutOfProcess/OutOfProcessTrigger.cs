using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Manager.Service.Services.History.Commands.RecordEvent;
using MediatR;
using MessageDispatcher.Contracts;
using Voyager.Api;

namespace Manager.Service.Services.PoC.OutOfProcess
{
    [VoyagerRoute(HttpMethod.Post, "api/Dispatch")]
    public class OutOfProcessTrigger : IRequest<Response>
    {
        internal class OutOfProcessTriggerHandler : IRequestHandler<OutOfProcessTrigger,Response>
        {
            private readonly IMediator _mediator;

            public OutOfProcessTriggerHandler(IMediator mediator)
            {
                _mediator = mediator;
            }

            public Task<Response> Handle(OutOfProcessTrigger request, CancellationToken cancellationToken)
            {
                _mediator.Enqueue("JobFromApi",new RecordEvent()
                {
                    Message = "Record event out of process",
                    EntityType = "IRequest",
                    EventName = "OutOfProcessRequest",
                    EntityPrimaryKey = Guid.NewGuid().ToString()
                });
                
                return Task.FromResult(Response.Ok());
            }
        }
    }
}