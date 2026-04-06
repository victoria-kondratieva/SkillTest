using SkillTest.Domain.Exceptions;

namespace SkillTest.Application.Common.Exceptions;

public sealed class RegistrationFailedException : DomainException
{
    public RegistrationFailedException(string message)
        : base(message) { }
}
