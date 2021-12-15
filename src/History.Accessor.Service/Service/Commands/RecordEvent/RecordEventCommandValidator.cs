using FluentValidation;

namespace History.Accessor.Service.Service.Commands.RecordEvent
{
    public class RecordEventCommandValidator : AbstractValidator<Contracts.Commands.RecordEventCommand>
    {
        public RecordEventCommandValidator()
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