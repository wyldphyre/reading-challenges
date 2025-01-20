using Challenges.Application.Database;
using Challenges.Application.Repositories;
using Challenges.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Challenges.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IChallengeRepository, ChallengeRepository>();
        services.AddSingleton<IChallengeService, ChallengeService>();
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new NpgsqlConnectionFactory(connectionString));
        services.AddSingleton<DbInitializer>();
        return services;
    }
}