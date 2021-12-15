using System;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Hangfire;
using MediatR;
using MessageDispatcher.Contracts;
using Microsoft.Extensions.Logging;

namespace MessageDispatcher.Infrastructure;

public class HangFireDispatcher : IMessageDispatcher
{
    private readonly ILogger<HangFireDispatcher> _logger;

    public HangFireDispatcher(ILogger<HangFireDispatcher> logger)
    {
        _logger = logger;
    }

    public Response Dispatch(string jobIdentifier, IRequest request)
    {
        try
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatRBridge>(x => x.Send(jobIdentifier,request));
            return Response.Ok();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e,e.Message);
            return Response.Fail(e.Message);
        }
    }

    public Response Dispatch<T>(string jobIdentifier, IRequest<T> request)
    {
        try
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatRBridge>(x => x.Send(jobIdentifier,request));
            return Response.Ok();
        }
        catch (Exception e)
        {
            _logger.LogCritical(e,e.Message);
            return Response.Fail(e.Message);
        }
    }
}