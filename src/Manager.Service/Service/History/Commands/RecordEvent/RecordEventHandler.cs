using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HistoryAccessor.Contracts;
using HistoryAccessor.ServiceLevelContracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Utilities.MediatRPipeline.ExceptionHandling;
using Voyager;

namespace Orchestra.Manager.Service.History.Commands.RecordEvent
{
    internal class RecordEventHandler : IRequestHandler<RecordEvent, Response>, IInjectHttpContext
    {
        private readonly IHistoryAccessor _historyAccessor;

        private readonly IMapper _mapper;

        public HttpContext HttpContext { get; set; }
        
        public RecordEventHandler(IHistoryAccessor historyAccessor, IMapper mapper)
        {
            _historyAccessor = historyAccessor;
            _mapper = mapper;
        }
        
        public async Task<Response> Handle(RecordEvent request, CancellationToken cancellationToken)
        {
            var operation = await _historyAccessor.RecordEvent(_mapper.Map<EventDto>(request), cancellationToken);
            operation
                .OnSuccess(() =>
                {
                    // Do some simple logic
                    Console.WriteLine("The execution was successful");
                })
                .OnFailure(() =>
                {
                    // Do some simple logic
                    Console.WriteLine("The execution was unsuccessful");
                });
            
            return operation;
        }

    }
}