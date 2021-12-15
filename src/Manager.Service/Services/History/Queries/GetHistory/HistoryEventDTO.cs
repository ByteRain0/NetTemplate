using System;

namespace Manager.Service.Services.History.Queries.GetHistory;

public class HistoryEventDTO
{
    public Guid Id { get; set; }
        
    public DateTimeOffset DateTime { get; set; }

    /// <summary>
    /// User friendly message to show to the user.
    /// </summary>
    public string Message { get; set; }
        
    /// <summary>
    /// Id of the user that triggered the event.
    /// Is left empty in case of stateless API.
    /// </summary>
    public string UserId { get; set; }
        
    public string UserName { get; set; }
        
    /// <summary>
    /// PK of the affected entity.
    /// </summary>
    public string EntityPrimaryKey { get; set; }

    /// <summary>
    /// Name of the entity type, used for grouping events.
    /// </summary>
    public string EntityType { get; set; }
        
    public string EventName { get; set; }
}