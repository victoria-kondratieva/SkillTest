using Microsoft.EntityFrameworkCore;

using SkillTest.Domain.Tests.Entities;
using SkillTest.Domain.Users.Entities;
using SkillTest.Domain.Tests.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.Identifiers;
using SkillTest.Domain.Users.ValueObjects.User;

namespace SkillTest.Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static void Apply(ModelBuilder builder)
    {
        var adminId = UserId.From(Guid.Parse("3f4c2c8c-1c0e-4e4e-9b8e-0f7a4b6d2c11"));

        builder.Entity<Category>().HasData(
            new
            {
                Id = CategoryId.From(Guid.Parse("a8d9f3b2-7c44-4c1e-9f0a-2e6b1d9a8f55")),
                Name = "Programming",
                Description = "All programming-related topics",
                Color = "#FF5733",
                Icon = "code"
            },
            new
            {
                Id = CategoryId.From(Guid.Parse("c1e7a2d4-9b33-4f0d-8c77-1a2b3c4d5e66")),
                Name = "Networking",
                Description = "Network technologies and protocols",
                Color = "#33A1FF",
                Icon = "network"
            },
            new
            {
                Id = CategoryId.From(Guid.Parse("5b9e1f22-3c8d-4f7a-9d11-7c8e2a1b4f99")),
                Name = "DevOps",
                Description = "CI/CD, automation, infrastructure",
                Color = "#33FF57",
                Icon = "devops"
            },
            new
            {
                Id = CategoryId.From(Guid.Parse("d4a1c7e8-2b55-4f33-8e0c-9a1d2b3c4e77")),
                Name = "Databases",
                Description = "SQL, NoSQL, data modeling",
                Color = "#FF33A8",
                Icon = "database"
            },
            new
            {
                Id = CategoryId.From(Guid.Parse("9c7a4e11-2d3f-4b8a-9f55-6e7d8c9b1a44")),
                Name = "Web Development",
                Description = "Frontend, backend, web technologies",
                Color = "#FFC300",
                Icon = "web"
            }
        );

        builder.Entity<User>().HasData(
            new
            {
                Id = adminId,
                TotalPoints = Points.Zero
            }
        );

        builder.Entity<User>().OwnsOne(u => u.Email).HasData(
            new
            {
                UserId = adminId,
                Value = "admin@example.com"
            }
        );

        builder.Entity<User>().OwnsOne(u => u.Profile).HasData(
            new
            {
                UserId = adminId,
                Position = "Administrator",
                AvatarUrl = ""
            }
        );

        builder.Entity<User>().OwnsOne(u => u.Profile)
            .OwnsOne(p => p.Username).HasData(
                new
                {
                    UserProfileUserId = adminId,
                    Value = "admin"
                }
            );

        builder.Entity<User>().OwnsOne(u => u.Profile)
            .OwnsOne(p => p.FullName).HasData(
                new
                {
                    UserProfileUserId = adminId,
                    FirstName = "Admin",
                    LastName = "Admin"
                }
            );

        builder.Entity<User>().OwnsOne(u => u.Settings).HasData(
            new
            {
                UserId = adminId,
                EmailNotificationsEnabled = true,
                TimeLimitSeconds = 0,
                AutoAdvanceEnabled = false,
                Language = "ru"
            }
        );
    }
}