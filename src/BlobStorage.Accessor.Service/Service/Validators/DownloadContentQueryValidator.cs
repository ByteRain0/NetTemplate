using BlobStorage.Accessor.Contracts.Queries;
using FluentValidation;

namespace BlobStorage.Accessor.Service.Service.Validators;

public class DownloadContentQueryValidator : AbstractValidator<DownloadContentQuery>
{
    public DownloadContentQueryValidator()
    {
        RuleFor(x => x.Path)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty();
    }
}