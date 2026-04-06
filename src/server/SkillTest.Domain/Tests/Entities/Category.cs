using SkillTest.Domain.Primitives;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;

namespace SkillTest.Domain.Tests.Entities;

public sealed class Category : Entity<CategoryId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Color { get; private set; }
    public string Icon { get; private set; }

    private Category() { }

    public Category(
        CategoryId id,
        string name,
        string description,
        string color,
        string icon)
        : base(id)
    {
        Name = name;
        Description = description;
        Color = color;
        Icon = icon;
    }

    public void Update(
        string name,
        string description,
        string color,
        string icon)
    {
        Name = name;
        Description = description;
        Color = color;
        Icon = icon;
    }
}
