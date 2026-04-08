using SkillTest.Application.Common.Models;

namespace SkillTest.Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(
        string email,
        string password,
        string username,
        string firstName,
        string lastName,
        CancellationToken ct = default);

    Task<AuthResult> LoginAsync(
        string email,
        string password,
        CancellationToken ct = default);
}
