using Microsoft.AspNetCore.Mvc;

using SkillTest.Api.Contracts.Auth.Requests;
using SkillTest.Api.Contracts.Auth.Responses;
using SkillTest.Application.Common.Interfaces;

namespace SkillTest.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="request">
    /// Registration data including email, password, username, first name and last name.
    /// </param>
    /// <returns>
    /// Returns an authentication response containing the user identifier, email and JWT access token.
    /// </returns>
    /// <response code="200">
    /// User successfully registered and authenticated. The response contains a valid JWT token.
    /// </response>
    /// <response code="400">
    /// The request is invalid (e.g. weak password, invalid email format, missing fields) or a user with the same email/username already exists.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred while processing the registration.
    /// </response>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(
            request.Email,
            request.Password,
            request.Username,
            request.FirstName,
            request.LastName
        );

        return Ok(new AuthResponse(
            result.UserId,
            result.Email,
            result.Token
        ));
    }

    /// <summary>
    /// Authenticates a user with email and password.
    /// </summary>
    /// <param name="request">
    /// Login credentials containing the user's email and password.
    /// </param>
    /// <returns>
    /// Returns an authentication response containing the user identifier, email and JWT access token.
    /// </returns>
    /// <response code="200">
    /// Login successful. The response contains a valid JWT token for subsequent authenticated requests.
    /// </response>
    /// <response code="400">
    /// The request payload is invalid (e.g. missing email or password).
    /// </response>
    /// <response code="401">
    /// Authentication failed due to invalid email or password.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred while processing the login.
    /// </response>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(
            request.Email,
            request.Password
        );

        return Ok(new AuthResponse(
            result.UserId,
            result.Email,
            result.Token
        ));
    }

    /// <summary>
    /// Checks whether the current request is authenticated.
    /// </summary>
    /// <returns>
    /// Returns a simple status indicating whether the current user is authenticated.
    /// </returns>
    /// <response code="200">
    /// The current request is authenticated and the user identity is valid.
    /// </response>
    /// <response code="401">
    /// The current request is not authenticated or the token is missing/invalid/expired.
    /// </response>
    [HttpGet("check")]
    public IActionResult CheckAuth()
    {
        if (User.Identity?.IsAuthenticated == true)
            return Ok("Authenticated");

        return Unauthorized("Not authenticated");
    }
}
