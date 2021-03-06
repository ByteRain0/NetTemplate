using FluentValidation;

namespace History.Accessor.Service.Service.Commands.RecordEvent
{
    public class RecordEventValidator : AbstractValidator<Contracts.Commands.RecordEvent>
    {
        public RecordEventValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty();

            RuleFor(x => x.EntityType)
                .NotEmpty();

            RuleFor(x => x.EventName)
                .NotEmpty();

            RuleFor(x => x.EntityPrimaryKey)
                .NotEmpty();
        }
    }
}