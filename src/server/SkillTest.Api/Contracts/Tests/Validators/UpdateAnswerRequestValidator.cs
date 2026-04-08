using FluentValidation;
using SkillTest.Api.Contracts.Tests.Requests;

namespace SkillTest.Api.Contracts.Tests.Validators;

public sealed class UpdateAnswerRequestValidator : AbstractValidator<UpdateAnswerRequest>
{
    public UpdateAnswerRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.IsCorrect)
            .NotNull();
    }
}
