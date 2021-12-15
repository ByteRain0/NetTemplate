using System;
using System.Collections.Generic;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.DTO_s;
using MediatR;

namespace History.Accessor.Contracts.Queries;

public class GetEventsQuery : IRequest<Response<EventOverviewDto>>
{
    public int Take { get; set; } = Int32.MaxValue;

    public int Skip { get; set; } = 0;
        
    public List<string> Events { get; set; } = new List<string>();
        
    public List<string> EntityTypes { get; set; } = new List<string>();
        
    public Guid? UserId { get; set; }

    public List<string> EntityPrimaryKeys { get; set; }

    /// <summary>
    /// Can be used to search inside the messages will be operation OR ELSE based on entity primary keys.
    /// Global search.
    /// </summary>
    public string OrElseSearchValue { get; set; }

    /// <summary>
    /// Can be used to search inside the messages will be operation AND ELSE.
    /// Filtering.
    /// </summary>
    public string AndContainsSearchValue { get; set; }
}