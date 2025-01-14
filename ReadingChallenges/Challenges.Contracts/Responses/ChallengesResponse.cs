namespace Challenges.Contracts.Responses;

public class ChallengesResponse
{
    public required IEnumerable<ChallengeResponse> Challenges { get; init; } = Enumerable.Empty<ChallengeResponse>();
}