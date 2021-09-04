using System.Threading;
using System.Threading.Tasks;
using History.Accessor.Contracts.Contracts;
using History.Accessor.Contracts.ServiceLevelContracts;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace History.Accessor.Service.Infrastructure.HealthChecks
{
    public class HistoryAccessorHealthCheck : IHealthCheck
    {
        private readonly IHistoryAccessor _historyAccessor;
        
        public HistoryAccessorHealthCheck(IHistoryAccessor historyAccessor)
        {
            _historyAccessor = historyAccessor;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var operation = await _historyAccessor.GetEvents(new EventOverviewFilter(), cancellationToken);
            
            if (operation.IsSuccess)
            {
                return HealthCheckResult.Healthy("A healthy result.");
            }
            
            return new HealthCheckResult(context.Registration.FailureStatus,
                "An unhealthy result.");
        }
    }
}