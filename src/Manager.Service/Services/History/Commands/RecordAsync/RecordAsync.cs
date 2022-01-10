using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.History.Commands.RecordAsync;

[VoyagerRoute(HttpMethod.Post,"api/recordEventAsync")]
public class RecordAsync : IRequest<Response>
{
    public string Message { get; set; }
        
    public string EntityPrimaryKey { get; set; }
        
    public string EntityType { get; set; }

    public string EventName { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }
}