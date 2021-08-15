using MediatR;
using Utilities.MediatRPipeline.ExceptionHandling;
using Utilities.MediatRPipeline.Retry;
using Voyager.Api;

namespace Orchestra.Manager.Service.History.Commands.RecordEvent
{
    [VoyagerRoute(HttpMethod.Post,"api/recordEvent")]
    public class RecordEvent : IRequest<Response>, IRetryMarker
    {
        public string Message { get; set; }
        
        public string EntityPrimaryKey { get; set; }
        
        public string EntityType { get; set; }

        public string EventName { get; set; }
    }
}