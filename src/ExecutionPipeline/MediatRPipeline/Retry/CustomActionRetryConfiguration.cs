namespace ExecutionPipeline.MediatRPipeline.Retry;

public class CustomActionRetryConfiguration
{
    public string Name { get; set; }
        
    public int RetryCount { get; set; }

    public int IncrementalCount { get; set; }
}