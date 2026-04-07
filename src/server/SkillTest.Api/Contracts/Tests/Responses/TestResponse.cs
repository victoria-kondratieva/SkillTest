namespace SkillTest.Api.Contracts.Tests.Responses;

public sealed record TestResponse(
    Guid Id,
    string Name,
    string Description,
    int? Duration,
    string Status,
    int MaxScore,
    string DifficultyLevel,
    Guid CategoryId,
    string CategoryName,
    Guid CreatedBy,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    IReadOnlyList<QuestionResponse> Questions,
    IReadOnlyList<TagResponse> Tags
);