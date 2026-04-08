using SkillTest.Application.Common.Interfaces;
using SkillTest.Application.Users.Enums;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;

namespace SkillTest.Application.Users;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityUserService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IIdentityUserService identityService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<User>> GetAllAsync(
        CancellationToken ct = default)
        => _userRepository.GetAllAsync(ct);

    public Task<User?> GetByIdAsync(
        UserId id, 
        CancellationToken ct = default)
        => _userRepository.GetByIdAsync(id, ct);

    public Task<User?> GetByEmailAsync(
        string email, 
        CancellationToken ct = default)
        => _userRepository.GetByEmailAsync(email, ct);

    public async Task CreateAsync(
        User user, 
        CancellationToken ct = default)
    {
        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<(bool Success, User? User)> AddPointsAsync(
        UserId id,
        int amount,
        TransactionReason reason,
        CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user is null)
            return (false, null);

        user.AddPoints(amount, reason);

        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync(ct);

        return (true, user);
    }

    public async Task<bool> DeleteUserAsync(
        Guid id, 
        CancellationToken ct = default)
    {
        var domainUser = await _userRepository.GetByIdAsync(UserId.From(id), ct);
        if (domainUser is null)
            return false;

        _userRepository.Delete(domainUser);
        await _unitOfWork.SaveChangesAsync(ct);

        await _identityService.DeleteIdentityUserAsync(id, ct);

        return true;
    }

    public Task<bool> AssignRoleAsync(
        Guid id, 
        UserRole role, 
        CancellationToken ct = default)
        => _identityService.AssignRoleAsync(id, role, ct);
}
