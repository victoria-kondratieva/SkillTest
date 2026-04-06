using System.Text.RegularExpressions;

using SkillTest.Domain.Primitives;
using SkillTest.Domain.Exceptions;

namespace SkillTest.Domain.Users.ValueObjects.User;

public sealed class Username : ValueObject
{
    public string Value { get; }

    private Username() { }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("Username is required.");

        var normalized = value.Trim();

        if (normalized.Length < 3 || normalized.Length > 30)
            throw new ValidationException("Username must be between 3 and 30 characters.");

        if (!Regex.IsMatch(normalized, "^[a-zA-Z0-9_.]+$"))
            throw new ValidationException("Username can contain only letters, digits, '_' and '.'.");

        Value = normalized;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
