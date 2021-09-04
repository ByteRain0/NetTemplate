using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Session.Accessor.Service.Contracts;

namespace Session.Accessor.Service.Service.Infrastructure
{
    public class ExecutorAccessorLogger : IExecutorAccessor
    {
        private readonly ILogger<ExecutorAccessorLogger> _logger;

        private readonly IExecutorAccessor _accessor;
        
        public ExecutorAccessorLogger(ILogger<ExecutorAccessorLogger> logger, IExecutorAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
        }

        public ExecutorInfoDTO GetExecutor(HttpContext header)
        {
            _logger.LogInformation("Requested the executor id from the request header");
            var value = _accessor.GetExecutor(header);
            _logger.LogInformation("Finished processing the request");
            return value;
        }
    }
}