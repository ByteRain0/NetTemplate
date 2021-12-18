using FluentValidation;

namespace BlobStorage.Accessor.Contracts.Commands.UploadContent;

public class UploadItemCommandValidator : AbstractValidator<UploadItemCommand>
{
    public UploadItemCommandValidator()
    {
        RuleFor(x => x.Stream)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty();
        
        RuleFor(x => x.Extension)
            .NotEmpty();

        RuleFor(x => x.Tags)
            .NotEmpty();
    }
}