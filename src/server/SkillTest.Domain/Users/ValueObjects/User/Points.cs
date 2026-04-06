using SkillTest.Domain.Primitives;

namespace SkillTest.Domain.Users.ValueObjects.User;

public sealed class Points : ValueObject
{
    public int Value { get; }

    private Points() { }

    public Points(int value)
    {
        Value = value;
    }

    public static Points Zero => new Points(0);

    public Points Add(int amount) => new Points(Value + amount);

    public Points Subtract(int amount) => new Points(Value - amount);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
