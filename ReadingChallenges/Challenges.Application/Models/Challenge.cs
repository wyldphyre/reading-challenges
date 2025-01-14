namespace Challenges.Application.Models;

public class Challenge
{
    public Guid Id { get; init; }

    public string Name { get; set; } = string.Empty;

    public int Year { get; set; }

    public int Target { get; set; }

    public int Completed { get; set; } = 0;
}