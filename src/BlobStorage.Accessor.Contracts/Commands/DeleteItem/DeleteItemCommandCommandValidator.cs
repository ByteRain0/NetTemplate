using FluentValidation;

namespace BlobStorage.Accessor.Contracts.Commands.DeleteItem;

public class DeleteItemCommandCommandValidator : AbstractValidator<DeleteItemCommand>
{
    public DeleteItemCommandCommandValidator()
    {
        RuleFor(x => x.RelativePath)
            .NotEmpty();

        RuleFor(x => x.FileName)
            .NotEmpty();
    }
}