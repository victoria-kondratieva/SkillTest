using FluentValidation;
using SkillTest.Api.Contracts.Tests.Requests;

namespace SkillTest.Api.Contracts.Tests.Validators;

public class CreateFullQuestionRequestValidator 
    : AbstractValidator<CreateFullQuestionRequest>
{
    public CreateFullQuestionRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.Type)
            .NotEmpty();

        RuleFor(x => x.Points)
            .GreaterThan(0);

        RuleFor(x => x.Answers)
            .NotEmpty()
            .WithMessage("At least one answer is required.");

        RuleForEach(x => x.Answers)
            .SetValidator(new CreateAnswerRequestValidator());
    }
}
