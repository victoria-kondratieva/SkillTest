namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record CreateTestRequest(
    string Name,
    string Description,
    int? Duration,
    string DifficultyLevel,
    Guid CategoryId,
    Guid CreatedBy
);