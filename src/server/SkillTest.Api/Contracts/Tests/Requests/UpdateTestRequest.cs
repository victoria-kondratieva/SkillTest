namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record UpdateTestRequest(
    string Name,
    string Description,
    int? Duration,
    string Status,
    string DifficultyLevel,
    Guid CategoryId
);