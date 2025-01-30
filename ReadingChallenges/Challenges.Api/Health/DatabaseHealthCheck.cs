﻿using Challenges.Application.Database;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Challenges.Api.Health
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        public const string Name = "Database";

        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ILogger<DatabaseHealthCheck> _logger;

        public DatabaseHealthCheck(IDbConnectionFactory connectionFactory, ILogger<DatabaseHealthCheck> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                _ = await _connectionFactory.CreateConnectionAsync(cancellationToken);
                return HealthCheckResult.Healthy();
            }
            catch (Exception e)
            {
                const string errorMessage = "Database is unhealthy";

                _logger.LogError(errorMessage, e);
                return HealthCheckResult.Unhealthy(errorMessage, e);
            }
        }
    }
}
