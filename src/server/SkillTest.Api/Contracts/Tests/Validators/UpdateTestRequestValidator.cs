using FluentValidation;
using SkillTest.Api.Contracts.Tests.Requests;

namespace SkillTest.Api.Contracts.Tests.Validators;

public sealed class UpdateTestRequestValidator : AbstractValidator<UpdateTestRequest>
{
    public UpdateTestRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.Duration)
            .GreaterThan(0)
            .When(x => x.Duration.HasValue);

        RuleFor(x => x.Status)
            .NotEmpty();

        RuleFor(x => x.DifficultyLevel)
            .NotEmpty();

        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}
