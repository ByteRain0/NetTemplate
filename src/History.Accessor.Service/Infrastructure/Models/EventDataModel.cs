using System;

namespace History.Accessor.Service.Infrastructure.Models;

public class EventDataModel
{
    [Obsolete]
    public EventDataModel()
    {
        //
    }
        
    public EventDataModel(string message, string userId,
        string userName, string eventName, string entityPrimaryKey, string entityType)
    {
        this.Id = Guid.NewGuid();
        this.DateTime = DateTime.UtcNow;
        this.Message = message;
        this.UserId = userId;
        this.UserName = userName;
        this.EventName = eventName;
        this.EntityPrimaryKey = entityPrimaryKey;
        this.EntityType = entityType;
    }
        
    public Guid Id { get; private set; }

    public DateTime DateTime { get; private set; }

    public string Message { get; private set; }

    public string UserId { get; private set; }

    public string UserName { get; private set; }

    public string EventName { get; private set; }

    public string EntityPrimaryKey { get; private set; }

    public string EntityType { get; private set; }
}