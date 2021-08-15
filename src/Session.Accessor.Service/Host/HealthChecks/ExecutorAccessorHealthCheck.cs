using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SessionAccessor.Contracts;

namespace SessionAccessor.Host.HealthChecks
{
    public class ExecutorAccessorHealthCheck : IHealthCheck
    {
        private readonly IExecutorAccessor _executorAccessor;

        public ExecutorAccessorHealthCheck(IExecutorAccessor executorAccessor)
        {
            _executorAccessor = executorAccessor;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var executorId = _executorAccessor.GetExecutorId(default);
            var executorName = _executorAccessor.GetExecutorName(default);

            if (executorId is not null && executorName is not null)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }
            
            return Task.FromResult(
                new HealthCheckResult(context.Registration.FailureStatus,
                    "An unhealthy result."));
        }
    }
}