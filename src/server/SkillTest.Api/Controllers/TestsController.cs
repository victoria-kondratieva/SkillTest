using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillTest.Api.Contracts.Tests.Requests;
using SkillTest.Application.Common.Mappers;
using SkillTest.Application.Tests;
using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Test;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Api.Controllers;

[ApiController]
[Route("api/tests")]
public sealed class TestsController : ControllerBase
{
    private readonly ITestService _testService;

    public TestsController(ITestService testService)
    {
        _testService = testService;
    }

    /// <summary>
    /// Returns a list of all tests.
    /// </summary>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var tests = await _testService.GetAllAsync(cancellationToken);
        return Ok(tests.Select(TestMapper.ToResponse));
    }

    /// <summary>
    /// Returns a test by its unique identifier.
    /// </summary>
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(id), cancellationToken);

        if (test is null)
            return NotFound();

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Creates a new test.
    /// </summary>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTestRequest request, CancellationToken cancellationToken)
    {
        var test = new Test(
            TestId.CreateUnique(),
            request.Name,
            request.Description,
            request.Duration,
            TestStatus.Draft,
            DifficultyLevel.From(request.DifficultyLevel),
            CategoryId.From(request.CategoryId),
            UserId.From(request.CreatedBy)
        );

        await _testService.CreateAsync(test, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = test.Id.Value }, TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Updates an existing test.
    /// </summary>
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTestRequest request, CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(id), cancellationToken);

        if (test is null)
            return NotFound();

        test.Update(
            request.Name,
            request.Description,
            request.Duration,
            TestStatus.From(request.Status),
            DifficultyLevel.From(request.DifficultyLevel),
            CategoryId.From(request.CategoryId)
        );

        await _testService.UpdateAsync(test, cancellationToken);

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Deletes a test by its unique identifier.
    /// </summary>
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _testService.DeleteAsync(TestId.From(id), cancellationToken);
        return NoContent();
    }
}
