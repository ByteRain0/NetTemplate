using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.Commands.RecordEvent;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using History.Accessor.Service.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace History.Accessor.Service.Service.Commands.RecordEvent;

internal class RecordEventCommandHandler : IRequestHandler<RecordEventCommand, Response>
{
    private readonly IHistoryContext _context;

    private readonly ILogger<RecordEventCommandHandler> _logger;

    public RecordEventCommandHandler(IHistoryContext context, ILogger<RecordEventCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Response> Handle(RecordEventCommand model, CancellationToken cancellationToken)
    {
        throw new InvalidOperationException("Some exception");
        
        EventDataModel eventDataModel = new EventDataModel(
            message: model.Message,
            userId: model.UserId,
            userName: model.UserName,
            eventName: model.EventName,
            entityPrimaryKey: model.EntityPrimaryKey,
            entityType: model.EntityType);

        _logger.LogTrace("Recording event : EventData : '{EventData}'. to local database.",JsonConvert.SerializeObject(eventDataModel));

        await _context.Events.AddAsync(eventDataModel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);

        return Response.Ok();
    }
}