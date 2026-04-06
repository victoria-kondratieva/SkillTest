using SkillTest.Domain.Users.Entities;

namespace SkillTest.Application.Common.Mappers;

public static class UserMapper
{
    public static object ToResponse(User u) => new
    {
        id = u.Id.Value,
        email = u.Email.Value,
        profile = new
        {
            username = u.Profile.Username.Value,
            firstName = u.Profile.FullName.FirstName,
            lastName = u.Profile.FullName.LastName,
            fullName = u.Profile.FullName.ToString(),
            position = u.Profile.Position,
            avatarUrl = u.Profile.AvatarUrl
        },
        settings = new
        {
            emailNotificationsEnabled = u.Settings.EmailNotificationsEnabled,
            timeLimitSeconds = u.Settings.TimeLimitSeconds,
            autoAdvanceEnabled = u.Settings.AutoAdvanceEnabled,
            language = u.Settings.Language
        },
        totalPoints = u.TotalPoints.Value
    };

    public static object ToPointsResponse(User u)
    {
        var lastTransaction = u.PointTransactions
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new
            {
                id = t.Id.Value,
                amount = t.Amount,
                reason = t.Reason.Value,
                createdAt = t.CreatedAt
            })
            .FirstOrDefault();

        return new
        {
            totalPoints = u.TotalPoints.Value,
            lastTransaction
        };
    }
}