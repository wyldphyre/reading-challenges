using Challenges.Application.Models;
using Challenges.Application.Repositories;
using Challenges.Application.Validators;
using FluentValidation;

namespace Challenges.Application.Services;

public class ChallengeService : IChallengeService
{
    private readonly IChallengeRepository _challengeRepository;
    private readonly ChallengeValidator _challengeValidator;

    public ChallengeService(IChallengeRepository challengeRepository, ChallengeValidator challengeValidator)
    {
        _challengeRepository = challengeRepository;
        _challengeValidator = challengeValidator;
    }

    public async Task<bool> CreateAsync(Challenge challenge, CancellationToken token = default)
    {
        await _challengeValidator.ValidateAndThrowAsync(challenge, token);

        return await _challengeRepository.CreateAsync(challenge, token);
    }

    public Task<Challenge?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return _challengeRepository.GetByIdAsync(id, token);
    }

    public Task<IEnumerable<Challenge>> GetAllAsync(CancellationToken token = default)
    {
        return _challengeRepository.GetAllAsync(token);
    }

    public async Task<Challenge?> UpdateAsync(Challenge challenge, CancellationToken token = default)
    {
        await _challengeValidator.ValidateAndThrowAsync(challenge, cancellationToken: token);

        var challengeExists = await _challengeRepository.ExistsByIdAsync(challenge.Id, token);

        if (!challengeExists)
        {
            return null;
        }

        await _challengeRepository.UpdateAsync(challenge, token);

        return challenge;
    }

    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
    {
        return _challengeRepository.DeleteByIdAsync(id, token);
    }
}