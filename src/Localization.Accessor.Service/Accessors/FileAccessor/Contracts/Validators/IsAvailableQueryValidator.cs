using FluentValidation;
using Localization.Accessor.Service.Accessors.FileAccessor.Contracts.Queries;

namespace Localization.Accessor.Service.Accessors.FileAccessor.Contracts.Validators
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