using System;
using System.Collections.Generic;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.History.Queries.GetHistory;

[VoyagerRoute(HttpMethod.Post,"api/GetHistory")]
public class GetHistory : IRequest<Response<History>>
{
    public int Take { get; set; } = Int32.MaxValue;

    public int Skip { get; set; } = 0;
        
    public List<string> Events { get; set; } = new List<string>();
        
    public List<string> EntityTypes { get; set; } = new List<string>();
        
    public Guid? UserId { get; set; }

    public List<string> EntityPrimaryKeys { get; set; }

    public string OrElseSearchValue { get; set; }
    
    public string AndContainsSearchValue { get; set; }
}