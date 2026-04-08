using Microsoft.AspNetCore.Mvc;
using SkillTest.Api.Contracts.TestAttempts.Requests;
using SkillTest.Application.Common.Mappers;
using SkillTest.Application.TestAttempts;
using SkillTest.Domain.TestAttempts.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Api.Controllers;

public class TestAttemptsController : ControllerBase
{
    private readonly ITestAttemptService _testAttemptService;

    public TestAttemptsController(ITestAttemptService testAttemptService)
    {
        _testAttemptService = testAttemptService;
    }

    /// <summary>
    /// Returns a test attempt by its unique identifier.
    /// </summary>
    /// <param name="id">
    /// The GUID of the test attempt to retrieve.
    /// </param>
    /// <param name="ct">
    /// Cancellation token for aborting the request.
    /// </param>
    /// <returns>
    /// Returns the test attempt with its details if found, otherwise a 404 Not Found response.
    /// </returns>
    /// <response code="200">
    /// The test attempt was successfully retrieved.
    /// </response>
    /// <response code="404">
    /// No test attempt with the specified identifier exists.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred while retrieving the test attempt.
    /// </response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAttempt(Guid id, CancellationToken ct)
    {
        var attempt = await _testAttemptService.GetByIdAsync(AttemptId.From(id), ct);
        if (attempt is null)
            return NotFound();

        return Ok(attempt);
    }

    /// <summary>
    /// Starts a new test attempt for a specific user and test.
    /// </summary>
    /// <param name="request">
    /// Contains the user identifier and test identifier required to start the attempt.
    /// </param>
    /// <param name="ct">
    /// Cancellation token for aborting the request.
    /// </param>
    /// <returns>
    /// Returns the created test attempt and a location header pointing to the attempt resource.
    /// </returns>
    /// <response code="201">
    /// The test attempt was successfully created.
    /// </response>
    /// <response code="400">
    /// The request is invalid or contains missing/incorrect data.
    /// </response>
    /// <response code="404">
    /// The specified user or test does not exist.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred while creating the test attempt.
    /// </response>
    [HttpPost("start")]
    public async Task<IActionResult> StartAttempt(
        [FromBody] StartAttemptRequest request, 
        CancellationToken ct)
    {
        var attempt = await _testAttemptService.CreateAsync(
            UserId.From(request.UserId),
            TestId.From(request.TestId),
            ct);

        var routeValues = new { id = attempt.Id.Value };
        return CreatedAtAction(nameof(GetAttempt), routeValues, attempt);
    }

    /// <summary>
    /// Marks an existing test attempt as finished.
    /// </summary>
    /// <param name="id">
    /// The GUID of the test attempt to finish.
    /// </param>
    /// <param name="ct">
    /// Cancellation token for aborting the request.
    /// </param>
    /// <returns>
    /// Returns a 204 No Content response on successful completion.
    /// </returns>
    /// <response code="204">
    /// The test attempt was successfully marked as finished.
    /// </response>
    /// <response code="404">
    /// No test attempt with the specified identifier exists.
    /// </response>
    /// <response code="409">
    /// The test attempt cannot be finished because it is already completed or cancelled.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred while finishing the test attempt.
    /// </response>
    [HttpPatch("{id:guid}/finish")]
    public async Task<IActionResult> FinishAttempt(Guid id, CancellationToken ct)
    {
        await _testAttemptService.FinishAsync(AttemptId.From(id), ct);
        return NoContent();
    }

    /// <summary>
    /// Cancels an existing test attempt.
    /// </summary>
    /// <param name="id">
    /// The GUID of the test attempt to cancel.
    /// </param>
    /// <param name="ct">
    /// Cancellation token for aborting the request.
    /// </param>
    /// <returns>
    /// Returns a 204 No Content response on successful cancellation.
    /// </returns>
    /// <response code="204">
    /// The test attempt was successfully cancelled.
    /// </response>
    /// <response code="404">
    /// No test attempt with the specified identifier exists.
    /// </response>
    /// <response code="409">
    /// The test attempt cannot be cancelled because it is already finished or cancelled.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred while cancelling the test attempt.
    /// </response>
    [HttpPatch("{id:guid}/cancel")]
    public async Task<IActionResult> CancelAttempt(Guid id, CancellationToken ct)
    {
        await _testAttemptService.CancelAsync(AttemptId.From(id), ct);
        return NoContent();
    }
}
