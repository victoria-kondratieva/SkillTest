namespace SkillTest.Api.Contracts.Tests.Responses;

public sealed record AnswerResponse(
    Guid Id,
    string Text,
    bool IsCorrect,
    int OrderIndex
);
