using System;
using System.Linq;
using System.Linq.Expressions;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using History.Accessor.Contracts.Contracts;
using History.Accessor.Service.Infrastructure.Models;
using MediatR;

namespace History.Accessor.Service.Service.Queries.GetEventsQuery
{
    public class GetEventsQuery : EventOverviewFilter, IRequest<Response<EventOverviewDto>>
    {
        public Expression<Func<EventDataModel, bool>> ActionFilter()
        {
            Expression<Func<EventDataModel, bool>> actionFilter = auditEntry =>
                Events == null || Events.All(string.IsNullOrEmpty) || !Events.Any() || Events.Contains(auditEntry.EventName);
            return actionFilter;
        }

        public Expression<Func<EventDataModel, bool>> EntityFilter()
        {
            Expression<Func<EventDataModel, bool>> entityFilter = auditEntry =>
                EntityTypes == null || EntityTypes.All(string.IsNullOrEmpty) || !EntityTypes.Any() || EntityTypes.Contains(auditEntry.UserName);
            return entityFilter;
        }

        public Expression<Func<EventDataModel, bool>> UserIdFilter()
        {
            Expression<Func<EventDataModel, bool>> userIdFilter = auditEntry =>
                (!UserId.HasValue || UserId == Guid.Empty) || UserId.Equals(auditEntry.UserId);
            return userIdFilter;
        }

        public Expression<Func<EventDataModel, bool>> EntityPkFilterWithOrElseOptionalMessage()
        {
            Expression<Func<EventDataModel, bool>> entityPkFilterWithOrElseOptionalMessage = auditEntry =>
                EntityPrimaryKeys == null ? string.IsNullOrEmpty(OrElseSearchValue) || auditEntry.Message.ToLower().Contains(OrElseSearchValue.ToLower())
                : !EntityPrimaryKeys.Any() ? string.IsNullOrEmpty(OrElseSearchValue) || auditEntry.Message.ToLower().Contains(OrElseSearchValue.ToLower())
                : EntityPrimaryKeys.Any(x => x == auditEntry.EntityPrimaryKey) || (string.IsNullOrEmpty(OrElseSearchValue) && auditEntry.Message.ToLower().Contains(OrElseSearchValue.ToLower()));
            return entityPkFilterWithOrElseOptionalMessage;
        }

        public Expression<Func<EventDataModel, bool>> EntityPkFilterWithAndContainsMessage()
        {
            Expression<Func<EventDataModel, bool>> entityPkFilterWithAndContainsMessage = auditEntry =>
                string.IsNullOrEmpty(AndContainsSearchValue) || auditEntry.Message.ToLower().Contains(AndContainsSearchValue.ToLower());
            return entityPkFilterWithAndContainsMessage;
        }


    }
}