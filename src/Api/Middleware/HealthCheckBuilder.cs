using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace Api.Middleware
{
    public static class HealthCheckBuilder
    {
        public static IEndpointRouteBuilder MapApplicationHealthChecks(
            this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true
                });

            return endpoints;
        }
    }
}
