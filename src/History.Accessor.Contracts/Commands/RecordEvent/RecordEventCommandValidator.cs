using FluentValidation;

namespace History.Accessor.Contracts.Commands.RecordEvent;

public class RecordEventCommandValidator : AbstractValidator<RecordEventCommand>
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