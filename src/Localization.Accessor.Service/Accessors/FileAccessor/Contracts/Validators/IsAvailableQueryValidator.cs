using FluentValidation;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Contracts
{
    public class IsAvailableQueryValidator : AbstractValidator<IsResourceAvailableQuery>
    {
        public IsAvailableQueryValidator()
        {
            RuleFor(x => x.Locale)
                .NotEmpty();
        }
    }
}