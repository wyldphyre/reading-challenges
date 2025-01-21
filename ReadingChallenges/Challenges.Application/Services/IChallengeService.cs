using Challenges.Application.Models;

namespace Challenges.Application.Services;

public interface IChallengeService
{
    Task<bool> CreateAsync(Challenge challenge, CancellationToken token);

    Task<Challenge?> GetByIdAsync(Guid id, CancellationToken token);

    Task<IEnumerable<Challenge>> GetAllAsync(CancellationToken token);

    Task<Challenge?> UpdateAsync(Challenge challenge, CancellationToken token);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken token);
}