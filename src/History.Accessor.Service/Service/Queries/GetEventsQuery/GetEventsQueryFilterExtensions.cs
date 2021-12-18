using System;
using System.Linq;
using System.Linq.Expressions;
using History.Accessor.Service.Infrastructure.Models;

namespace History.Accessor.Service.Service.Queries.GetEventsQuery;

public static class GetEventsQueryFilterExtensions
{
    public static Expression<Func<EventDataModel, bool>> ActionFilter(this Contracts.Queries.GetEventsQuery.GetEventsQuery query)
    {
        Expression<Func<EventDataModel, bool>> actionFilter = auditEntry =>
            query.Events == null || query.Events.All(string.IsNullOrEmpty) || !query.Events.Any() ||
            query.Events.Contains(auditEntry.EventName);
        return actionFilter;
    }

    public static Expression<Func<EventDataModel, bool>> EntityFilter(this Contracts.Queries.GetEventsQuery.GetEventsQuery query)
    {
        Expression<Func<EventDataModel, bool>> entityFilter = auditEntry =>
            query.EntityTypes == null || query.EntityTypes.All(string.IsNullOrEmpty) || !query.EntityTypes.Any() ||
            query.EntityTypes.Contains(auditEntry.UserName);
        return entityFilter;
    }

    public static Expression<Func<EventDataModel, bool>> UserIdFilter(this Contracts.Queries.GetEventsQuery.GetEventsQuery query)
    {
        Expression<Func<EventDataModel, bool>> userIdFilter = auditEntry =>
            (!query.UserId.HasValue || query.UserId == Guid.Empty) || query.UserId.Equals(auditEntry.UserId);
        return userIdFilter;
    }

    public static Expression<Func<EventDataModel, bool>> EntityPkFilterWithOrElseOptionalMessage(this Contracts.Queries.GetEventsQuery.GetEventsQuery query)
    {
        Expression<Func<EventDataModel, bool>> entityPkFilterWithOrElseOptionalMessage = auditEntry =>
            query.EntityPrimaryKeys == null ? string.IsNullOrEmpty(query.OrElseSearchValue) ||
                                              auditEntry.Message.ToLower().Contains(query.OrElseSearchValue.ToLower())
            : !query.EntityPrimaryKeys.Any() ? string.IsNullOrEmpty(query.OrElseSearchValue) ||
                                               auditEntry.Message.ToLower().Contains(query.OrElseSearchValue.ToLower())
            : query.EntityPrimaryKeys.Any(x => x == auditEntry.EntityPrimaryKey) ||
              (string.IsNullOrEmpty(query.OrElseSearchValue) &&
               auditEntry.Message.ToLower().Contains(query.OrElseSearchValue.ToLower()));
        return entityPkFilterWithOrElseOptionalMessage;
    }

    public static Expression<Func<EventDataModel, bool>> EntityPkFilterWithAndContainsMessage(this Contracts.Queries.GetEventsQuery.GetEventsQuery query)
    {
        Expression<Func<EventDataModel, bool>> entityPkFilterWithAndContainsMessage = auditEntry =>
            string.IsNullOrEmpty(query.AndContainsSearchValue) ||
            auditEntry.Message.ToLower().Contains(query.AndContainsSearchValue.ToLower());
        return entityPkFilterWithAndContainsMessage;
    }
}