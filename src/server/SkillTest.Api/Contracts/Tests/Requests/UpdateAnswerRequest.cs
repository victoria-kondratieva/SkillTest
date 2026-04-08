namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record UpdateAnswerRequest(
    string Text,
    bool IsCorrect
);