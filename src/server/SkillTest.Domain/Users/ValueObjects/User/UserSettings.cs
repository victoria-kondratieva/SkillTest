using SkillTest.Domain.Primitives;
using SkillTest.Domain.Exceptions;

public sealed class UserSettings : ValueObject
{
    public bool EmailNotificationsEnabled { get; }
    public int? TimeLimitSeconds { get; }
    public bool AutoAdvanceEnabled { get; }
    public string Language { get; }

    private UserSettings() { }

    public UserSettings(
        bool? emailNotificationsEnabled = null,
        int? timeLimitSeconds = null,
        bool? autoAdvanceEnabled = null,
        string? language = null)
    {
        if (timeLimitSeconds is < 0)
            throw new ValidationException("Time limit must be positive.");

        EmailNotificationsEnabled = emailNotificationsEnabled ?? false;
        TimeLimitSeconds = timeLimitSeconds;
        AutoAdvanceEnabled = autoAdvanceEnabled ?? false;
        Language = string.IsNullOrWhiteSpace(language) ? "ru" : language;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return EmailNotificationsEnabled;
        yield return TimeLimitSeconds;
        yield return AutoAdvanceEnabled;
        yield return Language;
    }
}
