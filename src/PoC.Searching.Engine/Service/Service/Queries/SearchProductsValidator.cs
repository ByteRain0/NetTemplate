using FluentValidation;

namespace Instruments.Searching.Engine.Contracts.Service.Service.Queries
{
    public class SearchProductsValidator : AbstractValidator<SearchProducts>
    {
        public SearchProductsValidator()
        {
            RuleFor(x => x.Region)
                .NotEmpty();

            RuleFor(x => x.Type)
                .NotEmpty();
        }
    }
}