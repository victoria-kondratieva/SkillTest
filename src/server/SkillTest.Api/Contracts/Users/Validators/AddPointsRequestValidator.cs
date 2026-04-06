using FluentValidation;
using SkillTest.Api.Contracts.Users.Requests;

namespace SkillTest.Api.Contracts.Users.Validators;

public sealed class AddPointsRequestValidator : AbstractValidator<AddPointsRequest>
{
    public AddPointsRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
