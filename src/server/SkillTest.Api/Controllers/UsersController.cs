using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SkillTest.Api.Contracts.Users.Requests;
using SkillTest.Application.Common.Mappers;
using SkillTest.Application.Users;

using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;
using SkillTest.Domain.Users.ValueObjects.User;

using SkillTest.Infrastructure.Identity.Roles;

namespace SkillTest.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Returns a list of all users.
    /// </summary>
    /// <returns>A collection of user objects.</returns>
    /// <response code="200">Successfully returned the list of users.</response>
    /// <response code="401">The request is unauthorized.</response>
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllAsync(cancellationToken);
        return Ok(users.Select(UserMapper.ToResponse));
    }

    /// <summary>
    /// Returns a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The user object if found.</returns>
    /// <response code="200">User found and returned.</response>
    /// <response code="404">User with the specified ID does not exist.</response>
    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(UserId.From(id), cancellationToken);
        if (user is null)
            return NotFound();

        return Ok(UserMapper.ToResponse(user));
    }

    /// <summary>
    /// Returns a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The user object if found.</returns>
    /// <response code="200">User found and returned.</response>
    /// <response code="404">No user exists with the specified email.</response>
    /// <response code="401">The request is unauthorized.</response>
    [Authorize]
    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail(
        [FromQuery] string email, 
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmailAsync(email, cancellationToken); 
        if (user is null)
            return NotFound();

        return Ok(UserMapper.ToResponse(user));
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">The user creation payload.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The created user object.</returns>
    /// <response code="201">User successfully created.</response>
    /// <response code="400">Invalid request payload.</response>
    /// <response code="401">The request is unauthorized.</response>
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request, 
        CancellationToken cancellationToken)
    {
        var user = new User(
            UserId.CreateUnique(),
            new Email(request.Email),
            new UserProfile(
                new Username(request.Username),
                new FullName(request.FirstName, request.LastName),
                request.Position,
                request.AvatarUrl
            ),
            new UserSettings(
                request.EmailNotificationsEnabled,
                request.TimeLimitSeconds,
                request.AutoAdvanceEnabled,
                request.Language
            )
        );

        await _userService.CreateAsync(user, cancellationToken);

        var response = UserMapper.ToResponse(user);
        var routeValues = new { id = user.Id.Value };

        return CreatedAtAction(nameof(GetById), routeValues, response);
    }

    /// <summary>
    /// Adds points to a user's balance and creates a point transaction.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="request">The amount and reason for adding points.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The updated total points and the latest transaction.</returns>
    /// <response code="200">Points successfully added.</response>
    /// <response code="400">Invalid request payload.</response>
    /// <response code="404">User not found.</response>
    /// <response code="401">The request is unauthorized.</response>
    [Authorize]
    [HttpPut("{id:guid}/add-points")]
    public async Task<IActionResult> AddPoints(
        Guid id, 
        [FromBody] AddPointsRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _userService.AddPointsAsync(
            UserId.From(id),
            request.Amount,
            TransactionReason.From(request.Reason),
            cancellationToken
        );

        if (!result.Success)
            return NotFound();

        return Ok(UserMapper.ToPointsResponse(result.User!));
    }

    /// <summary>
    /// Deletes a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content.</returns>
    /// <response code="204">User successfully deleted.</response>
    /// <response code="404">User not found.</response>
    /// <response code="401">The request is unauthorized.</response>
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var success = await _userService.DeleteUserAsync(id, cancellationToken);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Assigns a role to the specified user.
    /// </summary>
    /// <param name="id">The unique identifier of the user.</param>
    /// <param name="request">The role assignment request containing the target role.</param>
    /// <returns>A success message.</returns>
    /// <response code="200">Role successfully assigned to the user.</response>
    /// <response code="404">User not found.</response>
    /// <response code="401">The request is unauthorized.</response>
    /// <response code="403">The caller does not have permission to assign roles.</response>
    [Authorize(Roles = RoleNames.Admin)]
    [HttpPut("{id:guid}/assign-role")]
    public async Task<IActionResult> AssignRole(
        Guid id, 
        AssignRoleRequest request)
    {
        var success = await _userService.AssignRoleAsync(id, request.Role);

        if (!success)
            return NotFound();

        return Ok(new { Message = $"Role '{request.Role}' assigned." });
    }

}
