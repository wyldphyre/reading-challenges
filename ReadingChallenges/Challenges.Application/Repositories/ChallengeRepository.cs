using Challenges.Application.Database;
using Challenges.Application.Models;
using Dapper;

namespace Challenges.Application.Repositories;

public class ChallengeRepository : IChallengeRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ChallengeRepository(IDbConnectionFactory connectionFactory, CancellationToken token = default)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> CreateAsync(Challenge challenge, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var result = await connection.ExecuteAsync(new CommandDefinition("""
            insert into challenges (id, name, year, target, completed)
            values (@Id, @Name, @Year, @Target, @Completed)
            """, challenge, cancellationToken: token));

        return result > 0;
    }

    public async Task<Challenge?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var challenge = await connection.QuerySingleOrDefaultAsync<Challenge>(
            new CommandDefinition("""
                                  select * from challenges where id = @id
                                  """, new { id }, cancellationToken: token));

        return challenge;
    }

    public async Task<IEnumerable<Challenge>> GetAllAsync(CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var challenges = await connection.QueryAsync<Challenge>(new CommandDefinition("""select * from challenges""", token));

        return challenges;
    }

    public async Task<bool> UpdateAsync(Challenge challenge, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);

        var result = await connection.ExecuteAsync(new CommandDefinition(
            """
            update challenges
            set Name = @Name, Year = @Year, Target = @Target, Completed = @Completed
            where id = @Id
            """, challenge, cancellationToken: token));

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        var result = await connection.ExecuteAsync(
            new CommandDefinition("delete from challenges where id = @id", new { id }, cancellationToken: token));

        return result > 0;
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync(token);
        return await connection.ExecuteScalarAsync<bool>(
            new CommandDefinition("select count(1) from challenges where id = @id", new { id }, cancellationToken: token));
    }
}