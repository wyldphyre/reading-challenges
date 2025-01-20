using Challenges.Application.Models;
using FluentValidation;

namespace Challenges.Application.Validators;

public class ChallengeValidator : AbstractValidator<Challenge>
{
    public ChallengeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Year)
            .GreaterThanOrEqualTo(1900)
            .LessThanOrEqualTo(3000);

        RuleFor(x => x.Target)
            .GreaterThan(0);

        RuleFor(x => x.Completed)
            .GreaterThanOrEqualTo(0);
    }
}