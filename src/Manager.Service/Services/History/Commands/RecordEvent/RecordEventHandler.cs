using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Voyager;

namespace Manager.Service.Services.History.Commands.RecordEvent
{
    public class RecordEventHandler : IRequestHandler<RecordEvent, Response>
    {
        private readonly IHistoryAccessor _historyAccessor;

        private readonly IMapper _mapper;
        
        public RecordEventHandler(IHistoryAccessor historyAccessor, IMapper mapper)
        {
            _historyAccessor = historyAccessor;
            _mapper = mapper;
        }

        public async Task<Response> Handle(RecordEvent request, CancellationToken cancellationToken)
        {
            var act = await _historyAccessor.RecordEvent(_mapper.Map<global::History.Accessor.Contracts.Commands.RecordEvent>(request), cancellationToken);
            return act;
        }
    }
}