using SkillTest.Domain.Primitives;
using SkillTest.Domain.Exceptions;
using SkillTest.Domain.Users.ValueObjects.User;
using SkillTest.Domain.Users.ValueObjects.PointTransaction;
using SkillTest.Domain.Users.ValueObjects.Identifiers;

namespace SkillTest.Domain.Users.Entities;

public sealed class User : Entity<UserId>, IAggregateRoot
{
    public Email Email { get; private set; }
    public UserProfile Profile { get; private set; }
    public UserSettings Settings { get; private set; }
    public Points TotalPoints { get; private set; }

    private readonly List<PointTransaction> _pointTransactions = new();
    public IReadOnlyCollection<PointTransaction> PointTransactions => _pointTransactions.AsReadOnly();

    private User() { }

    public User(UserId id, Email email, UserProfile profile, UserSettings settings)
        : base(id)
    {
        Email = email;
        Profile = profile;
        Settings = settings;
        TotalPoints = Points.Zero;
    }

    public void AddPoints(int amount, TransactionReason reason)
    {
        if (amount <= 0)
            throw new ValidationException("Amount must be positive.");

        TotalPoints = TotalPoints.Add(amount);

        _pointTransactions.Add(
           PointTransaction.Create(Id, reason, amount)
        );
    }
}

