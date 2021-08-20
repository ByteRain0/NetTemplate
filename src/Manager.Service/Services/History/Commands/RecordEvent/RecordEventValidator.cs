using FluentValidation;

namespace Manager.Service.Services.History.Commands.RecordEvent
{
    public class RecordEventValidator : AbstractValidator<RecordEvent>
    {
        public RecordEventValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty();

            RuleFor(x => x.EntityType)
                .NotEmpty();

            RuleFor(x => x.EntityPrimaryKey)
                .NotEmpty();

            RuleFor(x => x.EventName)
                .NotEmpty();
        }
    }
}