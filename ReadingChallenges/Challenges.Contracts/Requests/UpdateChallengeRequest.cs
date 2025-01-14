namespace Challenges.Contracts.Requests;

public class UpdateChallengeRequest
{
    public required string Name { get; init; }

    public required int Year { get; init; }

    public required int Target { get; init; }

    public required int Completed { get; init; } = 0;
}