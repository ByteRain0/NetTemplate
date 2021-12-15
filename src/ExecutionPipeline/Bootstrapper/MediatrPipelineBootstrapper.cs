using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using ExecutionPipeline.MediatRPipeline.Loggers;
using ExecutionPipeline.MediatRPipeline.Retry;
using ExecutionPipeline.MediatRPipeline.Validator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExecutionPipeline.Bootstrapper;

public static class MediatrPipelineBootstrapper
{
    public static void AddExecutionPipeline(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatrPipelineBootstrapper));
        //Log method call
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogger<,>));
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