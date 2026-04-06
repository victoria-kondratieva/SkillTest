namespace SkillTest.Api.Contracts.Users.Requests;

public sealed record AddPointsRequest(
    int Amount,
    string Reason
);