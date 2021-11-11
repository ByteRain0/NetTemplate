using System.Threading;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Manager.Service.Services.Localization.Queries
{
    internal class RetrieveDefaultLocalizationHandler : IRequestHandler<RetrieveDefaultLocalization, Response<string>>
    {
        private readonly IStringLocalizer _localizer;

        public RetrieveDefaultLocalizationHandler(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public async Task<Response<string>> Handle(RetrieveDefaultLocalization request, CancellationToken cancellationToken)
        {
            var extractedValue = _localizer["default"];
            return Response.Ok<string>(extractedValue.Value);
        }
    }
}