using FluentValidation;
using SkillTest.Api.Contracts.Tests.Requests;

namespace SkillTest.Api.Contracts.Tests.Validators;

public sealed class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
{
    public UpdateQuestionRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Type)
            .NotEmpty();

        RuleFor(x => x.Points)
            .GreaterThan(0);
    }
}
