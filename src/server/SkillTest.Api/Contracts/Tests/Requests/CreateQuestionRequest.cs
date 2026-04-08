namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record CreateQuestionRequest(
    string Text,
    string Type,
    int Points
);
