using FluentValidation;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Contracts
{
    public class IsAvailableQueryValidator : AbstractValidator<IsAvailableQuery>
    {
        public IsAvailableQueryValidator()
        {
            RuleFor(x => x.Locale)
                .NotEmpty();
        }
    }
}