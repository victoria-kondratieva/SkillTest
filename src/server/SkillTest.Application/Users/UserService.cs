using SkillTest.Application.Common.Interfaces;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Application.Users.Enums;

namespace SkillTest.Application.Users;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityUserService _identityService;

    public UserService(
        IUserRepository userRepository,
        IIdentityUserService identityService)
    {
        _userRepository = userRepository;
        _identityService = identityService;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var domainUser = await _userRepository.GetByIdAsync(UserId.From(id));
        if (domainUser is null)
            return false;

        await _userRepository.DeleteAsync(domainUser);
        await _identityService.DeleteIdentityUserAsync(id);

        return true;
    }

    public async Task<bool> AssignRoleAsync(Guid id, UserRole role)
    {
        return await _identityService.AssignRoleAsync(id, role);
    }
}
