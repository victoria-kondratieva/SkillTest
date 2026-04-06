namespace SkillTest.Api.Contracts.Users.Requests;

public sealed record CreateUserRequest(
    string Email,
    string Username,
    string FirstName,
    string LastName,
    string Position,
    string AvatarUrl,
    bool? EmailNotificationsEnabled,
    int? TimeLimitSeconds,
    bool? AutoAdvanceEnabled,
    string? Language
);

