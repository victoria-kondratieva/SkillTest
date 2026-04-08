namespace SkillTest.Api.Contracts.Tests.Requests;

public sealed record CreateFullQuestionRequest(
    string Text,
    string Type,
    int Points,
    IReadOnlyList<CreateAnswerRequest> Answers
);
