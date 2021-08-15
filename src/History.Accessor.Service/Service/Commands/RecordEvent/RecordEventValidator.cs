using FluentValidation;

namespace HistoryAccessorService.Service.Commands.RecordEvent
{
    public class RecordEventValidator : AbstractValidator<RecordEvent>
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