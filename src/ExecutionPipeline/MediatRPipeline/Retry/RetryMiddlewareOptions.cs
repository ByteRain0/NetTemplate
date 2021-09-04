using System.Collections.Generic;

namespace ExecutionPipeline.MediatRPipeline.Retry
{
    public class RetryMiddlewareOptions
    {
        public int DefaultOperationRetryCount { get; set; } = 4;

        public int DefaultOperationIncrementalCount { get; set; } = 10;

        public List<CustomActionRetryConfiguration> CustomConfiguration { get; set; }
    }
}