using FluentValidation;
using Localization.Accessor.Service.Accessors.FileAccessor.Contracts.Queries;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Contracts.Validators;

public class GetLocalizationsQueryValidator : AbstractValidator<GetLocalizationsQuery>
{
    public GetLocalizationsQueryValidator()
    {
        RuleFor(x => x.Locale)
            .NotEmpty();
    }
}