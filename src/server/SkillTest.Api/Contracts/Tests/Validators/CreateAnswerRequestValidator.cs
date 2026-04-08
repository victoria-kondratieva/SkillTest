using FluentValidation;
using SkillTest.Api.Contracts.Tests.Requests;

namespace SkillTest.Api.Contracts.Tests.Validators;

public sealed class CreateAnswerRequestValidator : AbstractValidator<CreateAnswerRequest>
{
    public CreateAnswerRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.IsCorrect)
            .NotNull();
    }
}
