using System;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;

namespace MessageDispatcher.Contracts;

public interface IJobsAccessor
{
    Task<Response<DateTime>> GetLastSuccessfulExecution(string jobName);
}