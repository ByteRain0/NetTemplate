using FluentValidation;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Contracts
{
    public class GetLocalizationsQueryValidator : AbstractValidator<GetLocalizationsQuery>
    {
        public GetLocalizationsQueryValidator()
        {
            RuleFor(x => x.Locale)
                .NotEmpty();
        }
    }
}