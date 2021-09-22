using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts;
using History.Accessor.Contracts.Commands;
using History.Accessor.Contracts.Queries;
using History.Accessor.Service.Service.Commands.RecordEvent;
using History.Accessor.Service.Service.Queries.CountEvents;
using History.Accessor.Service.Service.Queries.GetEventsQuery;
using MediatR;

namespace History.Accessor.Service.Service
{
    public class HistoryAccessor : IHistoryAccessor
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;
        
        public HistoryAccessor(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Can be done by using an async bus ex : Azure Service Bus to post events that will
        /// be picked up by an Azure Function and asynchronously stored in a Database.
        /// But for simplicity sake we will just use an in memory message dispatcher like MediatR.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param> 
        /// <returns></returns>
        public async Task<Response> RecordEvent(RecordEvent request, CancellationToken cancellationToken)
        {
            var operation = await _mediator.Send(request:request,cancellationToken:cancellationToken);
            return operation;
        }
        
        public async Task<Response<EventOverviewDto>> GetEvents(GetEventsQuery query, CancellationToken cancellationToken)
        {
            var operation = await _mediator.Send(request:query,cancellationToken:cancellationToken);
            return operation;
        }
        
        public async Task<Response<int>> CountEvents(CountAuditEntries request, CancellationToken cancellationToken)
        {
            var operation = await _mediator.Send(request:request,cancellationToken:cancellationToken);
            return operation;
        }
    }
}