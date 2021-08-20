using FluentValidation;

namespace PoC.Searching.Engine.Service.Service.Queries
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