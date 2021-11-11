using FluentValidation;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor
{
    public class RemoveLocalizationCommandValidator : AbstractValidator<RemoveLocalizationCommand>
    {
        public RemoveLocalizationCommandValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty();

            RuleFor(x => x.Locale)
                .NotEmpty();
        }
    }
}