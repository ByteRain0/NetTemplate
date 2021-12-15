using FluentValidation;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Commands;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Validators;

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