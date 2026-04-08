using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SkillTest.Api.Contracts.Tests.Requests;
using SkillTest.Application.Common.Mappers;
using SkillTest.Application.Tests;
using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Question;
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
    /// <returns>A collection of test objects.</returns>
    /// <response code="200">Successfully returned the list of tests.</response>
    /// <response code="401">The request is unauthorized.</response>
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
    /// <param name="id">The unique identifier of the test.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The test object.</returns>
    /// <response code="200">Successfully returned the test.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test not found.</response>
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(id), cancellationToken);

        if (test is null)
            return NotFound();

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Creates a new test.
    /// </summary>
    /// <param name="request">The test creation data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The created test object.</returns>
    /// <response code="201">Successfully created the test.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">The request is unauthorized.</response>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTestRequest request, 
        CancellationToken cancellationToken)
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

        var response = TestMapper.ToResponse(test);
        var routeValues = new { id = test.Id.Value };

        return CreatedAtAction(nameof(GetById), routeValues, response);
    }

    /// <summary>
    /// Updates an existing test.
    /// </summary>
    /// <param name="id">The unique identifier of the test.</param>
    /// <param name="request">The updated test data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The updated test object.</returns>
    /// <response code="200">Successfully updated the test.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test not found.</response>
    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id, 
        [FromBody] UpdateTestRequest request, 
        CancellationToken cancellationToken)
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
    /// <param name="id">The unique identifier of the test.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <response code="204">Successfully deleted the test.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test not found.</response>
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _testService.DeleteAsync(TestId.From(id), cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Adds a new question to the specified test.
    /// </summary>
    /// <param name="testId">The unique identifier of the test.</param>
    /// <param name="request">The question creation data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The updated test object.</returns>
    /// <response code="200">Successfully added the question.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test not found.</response>
    [Authorize]
    [HttpPost("{testId:guid}/questions")]
    public async Task<IActionResult> AddQuestion(
        Guid testId,
        [FromBody] CreateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(testId), cancellationToken);
        if (test is null)
            return NotFound();

        var question = new Question(
            QuestionId.CreateUnique(),
            request.Text,
            QuestionType.From(request.Type),
            request.Points
        );

        test.AddQuestion(question);

        await _testService.UpdateAsync(test, cancellationToken);

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Updates an existing question inside the specified test.
    /// </summary>
    /// <param name="testId">The unique identifier of the test.</param>
    /// <param name="questionId">The unique identifier of the question.</param>
    /// <param name="request">The updated question data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The updated test object.</returns>
    /// <response code="200">Successfully updated the question.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test or question not found.</response>
    [Authorize]
    [HttpPut("{testId:guid}/questions/{questionId:guid}")]
    public async Task<IActionResult> UpdateQuestion(
        Guid testId,
        Guid questionId,
        [FromBody] UpdateQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(testId), cancellationToken);
        if (test is null)
            return NotFound();

        var question = test.Questions.FirstOrDefault(q => q.Id.Value == questionId);
        if (question is null)
            return NotFound();

        question.Update(
            request.Text,
            QuestionType.From(request.Type),
            request.Points
        );

        await _testService.UpdateAsync(test, cancellationToken);

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Deletes a question from the specified test.
    /// </summary>
    /// <param name="testId">The unique identifier of the test.</param>
    /// <param name="questionId">The unique identifier of the question.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <response code="204">Successfully deleted the question.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test or question not found.</response>
    [Authorize]
    [HttpDelete("{testId:guid}/questions/{questionId:guid}")]
    public async Task<IActionResult> DeleteQuestion(
        Guid testId,
        Guid questionId,
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(testId), cancellationToken);
        if (test is null)
            return NotFound();

        test.RemoveQuestion(QuestionId.From(questionId));

        await _testService.UpdateAsync(test, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Adds a new answer to the specified question inside the test.
    /// </summary>
    /// <param name="testId">The unique identifier of the test.</param>
    /// <param name="questionId">The unique identifier of the question.</param>
    /// <param name="request">The answer creation data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The updated test object.</returns>
    /// <response code="200">Successfully added the answer.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test or question not found.</response>
    [Authorize]
    [HttpPost("{testId:guid}/questions/{questionId:guid}/answers")]
    public async Task<IActionResult> AddAnswer(
        Guid testId,
        Guid questionId,
        [FromBody] CreateAnswerRequest request,
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(testId), cancellationToken);
        if (test is null)
            return NotFound();

        var question = test.Questions.FirstOrDefault(q => q.Id.Value == questionId);
        if (question is null)
            return NotFound();

        var answer = new Answer(
            AnswerId.CreateUnique(),
            request.Text,
            request.IsCorrect
        );

        question.AddAnswer(answer);

        await _testService.UpdateAsync(test, cancellationToken);

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Updates an existing answer inside the specified question.
    /// </summary>
    /// <param name="testId">The unique identifier of the test.</param>
    /// <param name="questionId">The unique identifier of the question.</param>
    /// <param name="answerId">The unique identifier of the answer.</param>
    /// <param name="request">The updated answer data.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The updated test object.</returns>
    /// <response code="200">Successfully updated the answer.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test, question, or answer not found.</response>
    [Authorize]
    [HttpPut("{testId:guid}/questions/{questionId:guid}/answers/{answerId:guid}")]
    public async Task<IActionResult> UpdateAnswer(
        Guid testId,
        Guid questionId,
        Guid answerId,
        [FromBody] UpdateAnswerRequest request,
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(testId), cancellationToken);
        if (test is null)
            return NotFound();

        var question = test.Questions.FirstOrDefault(q => q.Id.Value == questionId);
        if (question is null)
            return NotFound();

        var answer = question.Answers.FirstOrDefault(a => a.Id.Value == answerId);
        if (answer is null)
            return NotFound();

        answer.Update(request.Text, request.IsCorrect);

        await _testService.UpdateAsync(test, cancellationToken);

        return Ok(TestMapper.ToResponse(test));
    }

    /// <summary>
    /// Deletes an answer from the specified question inside the test.
    /// </summary>
    /// <param name="testId">The unique identifier of the test.</param>
    /// <param name="questionId">The unique identifier of the question.</param>
    /// <param name="answerId">The unique identifier of the answer.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <response code="204">Successfully deleted the answer.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="404">Test, question, or answer not found.</response>
    [Authorize]
    [HttpDelete("{testId:guid}/questions/{questionId:guid}/answers/{answerId:guid}")]
    public async Task<IActionResult> DeleteAnswer(
        Guid testId,
        Guid questionId,
        Guid answerId,
        CancellationToken cancellationToken)
    {
        var test = await _testService.GetByIdAsync(TestId.From(testId), cancellationToken);
        if (test is null)
            return NotFound();

        var question = test.Questions.FirstOrDefault(q => q.Id.Value == questionId);
        if (question is null)
            return NotFound();

        question.RemoveAnswer(AnswerId.From(answerId));

        await _testService.UpdateAsync(test, cancellationToken);

        return NoContent();
    }
}
