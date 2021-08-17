using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExecutionPipeline.MediatRPipeline.ExceptionHandling
{
    public class RequestExceptionWrapperMiddleware<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public RequestExceptionWrapperMiddleware(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (ValidationException e)
            {
                var response = JsonConvert.SerializeObject(Response.Fail(e.Message, HttpStatusCode.Forbidden));
                return JsonConvert.DeserializeObject<TResponse>(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error encountered while processing request '{request.ToString()}' error message : '{e.Message}'");
                var response = JsonConvert.SerializeObject(Response.Fail(e.Message));
                return JsonConvert.DeserializeObject<TResponse>(response);
            }
        }
    }
}