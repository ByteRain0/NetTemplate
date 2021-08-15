using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Utilities.MediatRPipeline.ExceptionHandling;
using Utilities.MediatRPipeline.Loggers;
using Utilities.MediatRPipeline.Retry;
using Utilities.MediatRPipeline.Validator;

namespace Utilities.Bootstrapper
{
    public static class MediatrPipelineBootstrapper
    {
        public static void ConfigureExecutionPipeline(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatrPipelineBootstrapper));
            //Log method call
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogger<>));
            //Start a timer to determine if the request is a long running one.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceMiddleware<,>));
            //Make sure the execution is safe.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionWrapperMiddleware<,>));
            //Validate the payload
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidator<,>));
            //Retry middleware in case some transient errors happen.
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestRetryMiddleware<,>));
        }
    }
}