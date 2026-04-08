namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record UpdateQuestionRequest(
    string Text,
    string Type,
    int Points
);