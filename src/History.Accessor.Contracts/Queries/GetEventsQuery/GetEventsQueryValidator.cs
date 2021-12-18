using FluentValidation;

namespace History.Accessor.Contracts.Queries.GetEventsQuery;

public class GetEventsQueryValidator : AbstractValidator<GetEventsQuery>
{
    public GetEventsQueryValidator()
    {
        RuleFor(x => x.Skip)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1);
    }
}