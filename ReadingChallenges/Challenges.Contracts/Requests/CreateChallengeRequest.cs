namespace Challenges.Contracts.Requests;

public class CreateChallengeRequest
{
    public required string Name { get; init; }

    public required int Year { get; init; }

    public required int Target { get; init; }

    public int Completed { get; init; } = 0;
}