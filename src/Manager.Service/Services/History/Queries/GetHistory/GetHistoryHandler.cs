using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts;
using History.Accessor.Contracts.Queries;
using MediatR;

namespace Manager.Service.Services.History.Queries.GetHistory;

internal class GetHistoryHandler : IRequestHandler<GetHistory, Response<History>>
{
    private readonly IHistoryAccessor _historyAccessor;

    private readonly IMapper _mapper;
    
    public GetHistoryHandler(IHistoryAccessor historyAccessor, IMapper mapper)
    {
        _historyAccessor = historyAccessor;
        _mapper = mapper;
    }

    public async Task<Response<History>> Handle(GetHistory request, CancellationToken cancellationToken)
    {
        var history = await _historyAccessor.GetEvents(_mapper.Map<GetEventsQuery>(source: request), cancellationToken: cancellationToken);

        if (history.IsFailure)
        {
            return Response.Fail<History>($"Error fetching history. Error : '{history.StackTrace}'", history.ResponseCode);
        }

        return Response.Ok(new History()
        {
            Events = _mapper.Map<List<HistoryEventDTO>>(source: history.Value.Events),
            Skipped = request.Skip,
            Taken = request.Take,
            TotalEventsCount = history.Value.Count
        });
    }
}