using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Runtime.CompilerServices;

namespace ProductCatalog.Api.HealthChecks;

public class LogHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        if (File.Exists("products.log"))
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
        else
            return Task.FromResult(HealthCheckResult.Degraded());  

    }
}
