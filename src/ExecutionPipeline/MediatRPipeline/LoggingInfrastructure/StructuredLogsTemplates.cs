namespace ExecutionPipeline.MediatRPipeline.LoggingInfrastructure;

public static class StructuredLogsTemplates
{
    public const string LongRunningRequestTemplate = "LongRunningRequest";
    
    public const string StartExecutionTemplate = "StartRequestExecution";
    
    public const string EndExecutionTemplate = "EndRequestExecution";
    
    public const string ExceptionEncounteredTemplate = "ExceptionEncountered";

    public const string RequestWasRetried = "RequestWasRetried";
}