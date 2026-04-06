using SkillTest.Domain.Primitives;
using SkillTest.Domain.Tests.ValueObjects.Test;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Tests.ValueObjects.Shared;

namespace SkillTest.Domain.Tests.Entities;

public sealed class Test : Entity<TestId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int? Duration { get; private set; }
    public TestStatus Status { get; private set; }
    public int MaxScore { get; private set; }
    public DifficultyLevel DifficultyLevel { get; private set; }

    public CategoryId CategoryId { get; private set; }
    public Category Category { get; private set; }

    private readonly List<Question> _questions = new();
    public IReadOnlyList<Question> Questions => _questions;

    private readonly List<Tag> _tags = new();
    public IReadOnlyList<Tag> Tags => _tags;

    public UserId CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Test() { }

    public Test(
        TestId id,
        string name,
        string description,
        int? duration,
        TestStatus status,
        int maxScore,
        DifficultyLevel difficultyLevel,
        CategoryId categoryId,
        UserId createdBy)
        : base(id)
    {
        Name = name;
        Description = description;
        Duration = duration;
        Status = status;
        MaxScore = maxScore;
        DifficultyLevel = difficultyLevel;
        CategoryId = categoryId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string description,
        int? duration,
        TestStatus status,
        int maxScore,
        DifficultyLevel difficultyLevel,
        CategoryId categoryId)
    {
        Name = name;
        Description = description;
        Duration = duration;
        Status = status;
        MaxScore = maxScore;
        DifficultyLevel = difficultyLevel;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddQuestion(Question question)
    {
        _questions.Add(question);
    }

    public void AddTag(Tag tag)
    {
        if (!_tags.Contains(tag))
            _tags.Add(tag);
    }
}
