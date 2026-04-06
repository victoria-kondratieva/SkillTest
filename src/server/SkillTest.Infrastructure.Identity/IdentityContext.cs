using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkillTest.Infrastructure.Identity.Entities;

namespace SkillTest.Infrastructure.Identity;

public class IdentityContext
    : IdentityDbContext<UserIdentity, RoleIdentity, Guid>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }
}
