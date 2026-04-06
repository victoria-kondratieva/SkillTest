using Microsoft.EntityFrameworkCore;
using SkillTest.Domain.TestAttempts.Entities;
using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Users.Entities;
using SkillTest.Infrastructure.Persistence.Seed;

namespace SkillTest.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();

    public DbSet<TestAttempt> TestAttempts => Set<TestAttempt>();
    public DbSet<TestResult> TestResults => Set<TestResult>();
    public DbSet<TestResultAnswer> TestResultAnswers => Set<TestResultAnswer>();

    public DbSet<User> Users => Set<User>();
    public DbSet<PointTransaction> PointTransactions => Set<PointTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        SeedData.Apply(builder);
    }
}
