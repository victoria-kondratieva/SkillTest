namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record CreateAnswerRequest(
    string Text,
    bool IsCorrect
);
