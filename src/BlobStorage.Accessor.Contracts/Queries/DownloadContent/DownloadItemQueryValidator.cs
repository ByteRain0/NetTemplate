using FluentValidation;

namespace BlobStorage.Accessor.Contracts.Queries.DownloadContent;

public class DownloadItemQueryValidator : AbstractValidator<DownloadItemQuery>
{
    public DownloadItemQueryValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty();
    }
}