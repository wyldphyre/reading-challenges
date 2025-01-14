using Challenges.Application.Database;
using Challenges.Application.Models;
using Dapper;

namespace Challenges.Application.Repositories;

public class ChallengeRepository : IChallengeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ChallengeRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(Challenge challenge)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into challenges (id, name, year, target, completed)
            values (@Id, @Name, @Year, @Target, @Completed)
            """, challenge));

        return result > 0;
    }

    public async Task<Challenge?> GetByIdAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var challenge = await connection.QuerySingleOrDefaultAsync<Challenge>(
            new CommandDefinition("""
                                  select * from challenges where id = @id
                                  """, new { id }));

        return challenge;
    }

    public async Task<IEnumerable<Challenge>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var challenges = await connection.QueryAsync<Challenge>(new CommandDefinition("""select * from challenges"""));

        return challenges;
    }

    public async Task<bool> UpdateAsync(Challenge challenge)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(new CommandDefinition(
            """
            update challenges
            set Name = @Name, Year = @Year, Target = @Target, Completed = @Completed
            where id = @Id
            """, challenge));

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(
            new CommandDefinition("delete from challenges where id = @id", new { id }));

        return result > 0;
    }

    public async Task<bool> ExistsById(Guid id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition("select count(1) from challenges where id = @id", new { id }));
    }
}