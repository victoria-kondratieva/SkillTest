using SkillTest.Domain.Primitives;
using SkillTest.Domain.Exceptions;

namespace SkillTest.Domain.Users.ValueObjects.User;

public sealed class UserProfile : ValueObject
{
    public Username Username { get; }
    public FullName FullName { get; }
    public string Position { get; }
    public string AvatarUrl { get; }

    private UserProfile() { }

    public UserProfile(Username username, FullName fullName, string position, string avatarUrl)
    {
        Username = username 
            ?? throw new ValidationException("Username is required.");
        FullName = fullName 
            ?? throw new ValidationException("Full name is required.");
        Position = position;
        AvatarUrl = avatarUrl;
    }

    public UserProfile(Username username, FullName fullName)
    {
        Username = username 
            ?? throw new ValidationException("Username is required.");
        FullName = fullName 
            ?? throw new ValidationException("Full name is required.");
        Position = string.Empty;
        AvatarUrl = string.Empty;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Username;
        yield return FullName;
        yield return Position;
        yield return AvatarUrl;
    }
}
