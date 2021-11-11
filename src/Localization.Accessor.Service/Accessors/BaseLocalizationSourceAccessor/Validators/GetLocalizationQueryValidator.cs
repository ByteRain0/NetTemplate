using FluentValidation;

namespace Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor
{
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
}