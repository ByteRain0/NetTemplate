using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using MediatR;
using Voyager.Api;

namespace Manager.Service.Services.Localization.Queries
{
    [VoyagerRoute( HttpMethod.Post,"api/RetrieveDefaultLocalization")]
    public class RetrieveDefaultLocalization : IRequest<Response<string>>
    {
        //
    }
}