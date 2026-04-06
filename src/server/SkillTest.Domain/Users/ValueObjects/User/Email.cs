using SkillTest.Domain.Exceptions;
using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Users.ValueObjects.User;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email() { }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email is required.");

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}