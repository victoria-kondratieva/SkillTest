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
        CancellationToken cancellationToken = default);

    Task<AuthResult> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
