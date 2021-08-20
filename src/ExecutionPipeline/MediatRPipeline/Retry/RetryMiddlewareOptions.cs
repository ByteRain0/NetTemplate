namespace ExecutionPipeline.MediatRPipeline.Retry
{
    public class RetryMiddlewareOptions
    {
        public int OperationRetryCount { get; set; } = 4;

        public int OperationIncrementalCount { get; set; } = 10;
    }
}