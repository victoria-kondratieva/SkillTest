using SkillTest.Domain.Exceptions;

namespace SkillTest.Application.Common.Exceptions;

public sealed class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException()
        : base("Invalid credentials.") { }
}
