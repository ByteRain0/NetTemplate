using System;
using System.Linq;
using System.Threading.Tasks;
using ExecutionPipeline.MediatRPipeline.ExceptionHandling;
using Hangfire;
using Hangfire.Storage;
using MessageDispatcher.Contracts;

namespace MessageDispatcher.Service;

public class JobsAccessor : IJobsAccessor
{
    public async Task<Response<DateTime>> GetLastSuccessfulExecution(string jobName)
    {
        using (var connection = JobStorage.Current.GetConnection())
        {
            var job = connection.GetRecurringJobs().FirstOrDefault(p => p.Id == jobName);

            var lastSuccessfulRun =
                JobStorage
                    .Current
                    .GetMonitoringApi()
                    .SucceededJobs(0, int.Parse(job.LastJobId))
                    .Where(x => x.Value.Job.Args[0].ToString() == jobName)
                    .OrderByDescending(x => x.Key)
                    .FirstOrDefault();

            if (lastSuccessfulRun.Value?.SucceededAt != null)
            {
                return Response.Ok(lastSuccessfulRun.Value.SucceededAt.Value);
            }
        }

        return Response.Ok(DateTime.MinValue);
    }
}