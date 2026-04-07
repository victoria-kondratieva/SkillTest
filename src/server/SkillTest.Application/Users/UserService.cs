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
        CancellationToken cancellationToken = default)
        => _userRepository.GetAllAsync(cancellationToken);

    public Task<User?> GetByIdAsync(
        UserId id, 
        CancellationToken cancellationToken = default)
        => _userRepository.GetByIdAsync(id, cancellationToken);

    public Task<User?> GetByEmailAsync(
        string email, 
        CancellationToken cancellationToken = default)
        => _userRepository.GetByEmailAsync(email, cancellationToken);

    public async Task CreateAsync(
        User user, 
        CancellationToken cancellationToken = default)
    {
        await _userRepository.AddAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<(bool Success, User? User)> AddPointsAsync(
        UserId id,
        int amount,
        TransactionReason reason,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user is null)
            return (false, null);

        user.AddPoints(amount, reason);

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return (true, user);
    }

    public async Task<bool> DeleteUserAsync(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var domainUser = await _userRepository.GetByIdAsync(UserId.From(id), cancellationToken);
        if (domainUser is null)
            return false;

        await _userRepository.DeleteAsync(domainUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _identityService.DeleteIdentityUserAsync(id, cancellationToken);

        return true;
    }

    public Task<bool> AssignRoleAsync(
        Guid id, 
        UserRole role, 
        CancellationToken cancellationToken = default)
        => _identityService.AssignRoleAsync(id, role, cancellationToken);
}
