using Challenges.Application.Models;

namespace Challenges.Application.Services;

public interface IChallengeService
{
    Task<bool> CreateAsync(Challenge challenge);

    Task<Challenge?> GetByIdAsync(Guid id);

    Task<IEnumerable<Challenge>> GetAllAsync();

    Task<Challenge?> UpdateAsync(Challenge challenge);

    Task<bool> DeleteByIdAsync(Guid id);
}