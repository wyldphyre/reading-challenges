using Challenges.Application.Models;
using Challenges.Contracts.Requests;
using Challenges.Contracts.Responses;

namespace Challenges.Api.Mapping;

public static class ContractMapping
{
    public static Challenge MapToChallenge(this CreateChallengeRequest request)
    {
        return new Challenge
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Year = request.Year,
            Target = request.Target,
            Completed = request.Completed
        };
    }

    public static Challenge MapToChallenge(this UpdateChallengeRequest request, Guid id)
    {
        return new Challenge
        {
            Id = id,
            Name = request.Name,
            Year = request.Year,
            Target = request.Target,
            Completed = request.Completed
        };
    }

    public static ChallengeResponse MapToResponse(this Challenge challenge)
    {
        return new ChallengeResponse
        {
            Id = challenge.Id,
            Name = challenge.Name,
            Year = challenge.Year,
            Target = challenge.Target,
            Completed = challenge.Completed
        };
    }

    public static ChallengesResponse MapToResponse(this IEnumerable<Challenge> challenges)
    {
        return new ChallengesResponse
        {
            Challenges = challenges.Select(MapToResponse)
        };
    }
}