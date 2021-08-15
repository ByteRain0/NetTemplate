using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SessionAccessor.Contracts;

namespace SessionAccessor.Service.Infrastructure
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

        public string GetExecutorId(HttpContext header)
        {
            _logger.LogInformation("Requested the executor id from the request header");
            var value = _accessor.GetExecutorId(header);
            _logger.LogInformation("Finished processing the request");
            return value;
        }

        public string GetExecutorName(HttpContext header)
        {
            _logger.LogInformation("Requested the executor name from the request header");
            var value = _accessor.GetExecutorName(header);
            _logger.LogInformation("Finished processing the request");
            return value;
        }
    }
}