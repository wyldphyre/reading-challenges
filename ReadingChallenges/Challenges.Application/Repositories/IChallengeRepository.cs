using Challenges.Application.Models;

namespace Challenges.Application.Repositories;

public interface IChallengeRepository
{
    Task<bool> CreateAsync(Challenge challenge);

    Task<Challenge?> GetByIdAsync(Guid id);

    Task<IEnumerable<Challenge>> GetAllAsync();

    Task<bool> UpdateAsync(Challenge challenge);

    Task<bool> DeleteByIdAsync(Guid id);

    Task<bool> ExistsByIdAsync(Guid id);
}