namespace SkillTest.Api.Contracts.Tests.Responses;

public sealed record QuestionResponse(
    Guid Id,
    string Text,
    string Type,
    int Points,
    int OrderIndex,
    IReadOnlyList<AnswerResponse> Answers,
    IReadOnlyList<TagResponse> Tags
);