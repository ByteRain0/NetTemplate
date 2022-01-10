using System.Collections.Generic;

namespace ExecutionPipeline.MediatRPipeline.Retry;

public class RetryMiddlewareOptions
{
    public int DefaultOperationRetryCount { get; set; } = 2;

    public int DefaultOperationIncrementalCount { get; set; } = 5;

    public List<CustomActionRetryConfiguration> CustomConfiguration { get; set; }
}