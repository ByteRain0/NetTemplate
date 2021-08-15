using System.Threading;
using System.Threading.Tasks;
using HistoryAccessorService.Infrastructure.DatabaseContext;
using HistoryAccessorService.Infrastructure.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Utilities.MediatRPipeline.ExceptionHandling;

namespace HistoryAccessorService.Service.Commands.RecordEvent
{
    internal class RecordEventHandler : IRequestHandler<RecordEvent, Response>
    {
        private readonly IHistoryContext _context;

        private readonly ILogger<RecordEventHandler> _logger;

        public RecordEventHandler(IHistoryContext context, ILogger<RecordEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        public async Task<Response> Handle(RecordEvent model, CancellationToken cancellationToken)
        {
            EventDataModel eventDataModel = new EventDataModel(
                message: model.Message,
                userId: model.UserId,
                userName: model.UserName,
                eventName: model.EventName);
            
            if (string.IsNullOrWhiteSpace(model.EntityPrimaryKey))
            {
                eventDataModel.AssignEntityToAudit(model.EntityPrimaryKey, entityType:model.EntityType);
            }
             
            _logger.LogTrace($"Recording event : '{JsonConvert.SerializeObject(eventDataModel)}' to local database.");
            
            await _context.Events.AddAsync(eventDataModel,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
            
            return Response.Ok();
        }
    }
}