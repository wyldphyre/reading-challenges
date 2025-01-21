using Challenges.Application.Models;

namespace Challenges.Application.Repositories;

public interface IChallengeRepository
{
    Task<bool> CreateAsync(Challenge challenge, CancellationToken token = default);

    Task<Challenge?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<IEnumerable<Challenge>> GetAllAsync(CancellationToken token = default);

    Task<bool> UpdateAsync(Challenge challenge, CancellationToken token = default);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default);

    Task<bool> ExistsByIdAsync(Guid id, CancellationToken token = default);
}