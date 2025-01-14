using Dapper;

namespace Challenges.Application.Database;

public class DbInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DbInitializer(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        await connection.ExecuteAsync("""
                                      create table if not exists challenges (
                                        id UUID primary key,
                                        name TEXT not null,
                                        year int not null,
                                        target int not null,
                                        completed int not null);
                                      """);
    }
}