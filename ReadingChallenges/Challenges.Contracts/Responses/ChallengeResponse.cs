namespace Challenges.Contracts.Responses;

public class ChallengeResponse
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required int Year { get; init; }

    public required int Target { get; init; }

    public required int Completed { get; init; } = 0;
}