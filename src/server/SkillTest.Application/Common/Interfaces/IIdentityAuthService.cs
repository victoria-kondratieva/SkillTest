namespace SkillTest.Application.Common.Interfaces;

public interface IIdentityAuthService
{
    Task<Guid> CreateIdentityUserAsync(string email, string password);
    Task<bool> ValidateCredentialsAsync(string email, string password);
    Task AssignRoleAsync(Guid userId, string roleName);
    Task<IReadOnlyList<string>> GetRolesAsync(Guid userId);
    Task<Guid> GetUserIdByEmailAsync(string email);
}
