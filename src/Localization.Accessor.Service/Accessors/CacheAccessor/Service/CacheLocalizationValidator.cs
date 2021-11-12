using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using FluentValidation;
using Localization.Accessor.Service.Accessors.BaseLocalizationSourceAccessor;
using Localization.Accessor.Service.Accessors.CacheAccessor.Contracts;
using Microsoft.Extensions.Localization;

namespace Localization.Accessor.Service.Accessors.CacheAccessor.Service
{
    public class CacheLocalizationValidator : ICacheLocalizationAccessor
    {
        private readonly ICacheLocalizationAccessor _instance;

        public CacheLocalizationValidator(ICacheLocalizationAccessor instance)
        {
            _instance = instance;
        }

        public async Task<Response<LocalizedString>> GetLocalizedString(GetLocalizationQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetLocalizationQueryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
            var operationResponse = await _instance.GetLocalizedString(request, cancellationToken);
            return operationResponse;
        }

        public async Task<Response> UpsertLocalization(UpsertLocalizationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpsertLocalizationCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
            var operationResponse = await _instance.UpsertLocalization(request, cancellationToken);
            return operationResponse;
        }

        public Task<Response<bool>> IsResourceAvailable(CancellationToken cancellationToken)
        {
            return _instance.IsResourceAvailable(cancellationToken);
        }

        public async Task<Response> RemoveLocalization(RemoveLocalizationCommand request, CancellationToken cancellationToken)
        {
            var validator = new RemoveLocalizationCommandValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);
            var operationResponse = await _instance.RemoveLocalization(request, cancellationToken);
            return operationResponse;
        }

        public Task<Response<string>> GetLanguagePreference(CancellationToken cancellationToken)
        {
            return _instance.GetLanguagePreference(cancellationToken);
        }
    }
}