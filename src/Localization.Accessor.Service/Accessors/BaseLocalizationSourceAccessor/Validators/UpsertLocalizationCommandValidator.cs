using FluentValidation;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Commands;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Validators
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