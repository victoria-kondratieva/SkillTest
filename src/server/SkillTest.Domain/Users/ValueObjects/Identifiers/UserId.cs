using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Users.ValueObjects.Identifiers;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId() { }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique() 
        => new UserId(Guid.NewGuid());
    public static UserId From(Guid value) 
        => new UserId(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}