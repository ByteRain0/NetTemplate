using FluentValidation;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Queries;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor.Validators;

public class GetLocalizationQueryValidator : AbstractValidator<GetLocalizationQuery>
{
    public GetLocalizationQueryValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty();

        RuleFor(x => x.Locale)
            .NotEmpty();
    }
}