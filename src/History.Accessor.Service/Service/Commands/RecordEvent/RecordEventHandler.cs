using System;
using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Service.Infrastructure.DatabaseContext;
using History.Accessor.Service.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace History.Accessor.Service.Service.Commands.RecordEvent
{
    internal class RecordEventHandler : IRequestHandler<Contracts.Commands.RecordEvent, Response>
    {
        private readonly IHistoryContext _context;

        private readonly ILogger<RecordEventHandler> _logger;

        public RecordEventHandler(IHistoryContext context, ILogger<RecordEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Response> Handle(Contracts.Commands.RecordEvent model, CancellationToken cancellationToken)
        {
            EventDataModel eventDataModel = new EventDataModel(
                message: model.Message,
                userId: model.UserId,
                userName: model.UserName,
                eventName: model.EventName,
                entityPrimaryKey: model.EntityPrimaryKey,
                entityType: model.EntityType);

            _logger.LogTrace($"Recording event : '{JsonConvert.SerializeObject(eventDataModel)}' to local database.");

            await _context.Events.AddAsync(eventDataModel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);

            return Response.Ok();
        }
    }
}