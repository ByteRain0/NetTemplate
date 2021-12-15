using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Session.Accessor.Service.Contracts;

namespace Session.Accessor.Service.Service.Infrastructure;

public class SessionInfoExtractorLogger : ISessionInfoExtractor
{
    private readonly ILogger<SessionInfoExtractorLogger> _logger;

    private readonly ISessionInfoExtractor _accessor;
        
    public SessionInfoExtractorLogger(ILogger<SessionInfoExtractorLogger> logger, ISessionInfoExtractor accessor)
    {
        _logger = logger;
        _accessor = accessor;
    }

    public ExecutorInfoDTO ExtractExecutorInformation(HttpContext header)
    {
        _logger.LogInformation("Requested the executor id from the request header");
        var value = _accessor.ExtractExecutorInformation(header);
        _logger.LogInformation("Finished processing the request");
        return value;
    }
}