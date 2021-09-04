using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.Contracts;
using History.Accessor.Contracts.ServiceLevelContracts;
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
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param> 
        /// <returns></returns>
        public async Task<Response> RecordEvent(EventDto model, CancellationToken cancellationToken)
        {
            var operation = await _mediator.Send(_mapper.Map<RecordEvent>(model),cancellationToken);
            return operation;
        }
        
        public async Task<Response<EventOverviewDto>> GetEvents(EventOverviewFilter filter, CancellationToken cancellationToken)
        {
            var operation = await _mediator.Send(_mapper.Map<GetEventsQuery>(filter),cancellationToken);
            return operation;
        }
        
        public async Task<Response<int>> CountEvents(CancellationToken cancellationToken)
        {
            var operation = await _mediator.Send(new CountAuditEntries(),cancellationToken);
            return operation;
        }
    }
}