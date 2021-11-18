using BlobStorage.Accessor.Contracts;
using BlobStorage.Accessor.Contracts.Commands;
using FluentValidation;

namespace BlobStorage.Accessor.Service.Service.Validators
{
    public class UploadContentCommandValidator : AbstractValidator<UploadContentCommand>
    {
        public UploadContentCommandValidator()
        {
            RuleFor(x => x.Stream)
                .NotEmpty();

            RuleFor(x => x.FileName)
                .NotEmpty();

            RuleFor(x => x.CustomPath)
                .NotEmpty();

            RuleFor(x => x.Extension)
                .NotEmpty();

            RuleFor(x => x.Tags)
                .NotEmpty();
        }
    }
}