using FluentValidation;
using SkillTest.Api.Contracts.Users.Requests;

namespace SkillTest.Api.Contracts.Users.Validators;

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.Position)
            .NotEmpty();

        RuleFor(x => x.AvatarUrl)
            .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
            .When(x => !string.IsNullOrWhiteSpace(x.AvatarUrl));

        RuleFor(x => x.TimeLimitSeconds)
            .GreaterThan(0)
            .When(x => x.TimeLimitSeconds.HasValue);
    }
}
