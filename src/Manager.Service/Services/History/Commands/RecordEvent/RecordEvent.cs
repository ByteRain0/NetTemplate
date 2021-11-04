using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Retry;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.History.Commands.RecordEvent
{
    [VoyagerRoute(HttpMethod.Post,"api/recordEvent")]
    public class RecordEvent : IRequest<Response>
    {
        public string Message { get; set; }
        
        public string EntityPrimaryKey { get; set; }
        
        public string EntityType { get; set; }

        public string EventName { get; set; }
    }
}