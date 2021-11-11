using FluentValidation;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor
{
    public class UpsertLocalizationCommandValidator : AbstractValidator<UpsertLocalizationCommand>
    {
        public UpsertLocalizationCommandValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty();

            RuleFor(x => x.Locale)
                .NotEmpty();

            RuleFor(x => x.Value)
                .NotEmpty();
        }
    }
}