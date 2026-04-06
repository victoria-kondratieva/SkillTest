using FluentValidation;
using SkillTest.Api.Contracts.Users.Requests;

namespace SkillTest.Api.Contracts.Users.Validators;

public sealed class AssignRoleRequestValidator : AbstractValidator<AssignRoleRequest>
{
    public AssignRoleRequestValidator()
    {
        RuleFor(x => x.Role)
            .IsInEnum();
    }
}
