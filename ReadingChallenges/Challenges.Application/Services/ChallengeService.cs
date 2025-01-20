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

    public async Task<bool> CreateAsync(Challenge challenge)
    {
        await _challengeValidator.ValidateAndThrowAsync(challenge);

        return await _challengeRepository.CreateAsync(challenge);
    }

    public Task<Challenge?> GetByIdAsync(Guid id)
    {
        return _challengeRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<Challenge>> GetAllAsync()
    {
        return _challengeRepository.GetAllAsync();
    }

    public async Task<Challenge?> UpdateAsync(Challenge challenge)
    {
        await _challengeValidator.ValidateAndThrowAsync(challenge);

        var challengeExists = await _challengeRepository.ExistsByIdAsync(challenge.Id);

        if (!challengeExists)
        {
            return null;
        }

        await _challengeRepository.UpdateAsync(challenge);

        return challenge;
    }

    public Task<bool> DeleteByIdAsync(Guid id)
    {
        return _challengeRepository.DeleteByIdAsync(id);
    }
}