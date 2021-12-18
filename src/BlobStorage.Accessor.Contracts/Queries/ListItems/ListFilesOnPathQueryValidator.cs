using FluentValidation;

namespace BlobStorage.Accessor.Contracts.Queries.ListItems;

public class ListFilesOnPathQueryValidator : AbstractValidator<ListFilesQuery>
{
    public ListFilesOnPathQueryValidator()
    {
        RuleFor(x => x.RelativePath)
            .NotEmpty();
    }
}