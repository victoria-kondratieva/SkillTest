namespace SkillTest.Api.Contracts.TestAttempts.Requests;

public record StartAttemptRequest(
    Guid UserId, 
    Guid TestId);