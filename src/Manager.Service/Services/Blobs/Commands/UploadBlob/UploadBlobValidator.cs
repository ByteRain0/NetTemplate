using FluentValidation;

namespace Manager.Service.Services.Blobs.Commands.UploadBlob
{
    public class UploadBlobValidator : AbstractValidator<UploadBlob>
    {
        public UploadBlobValidator()
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