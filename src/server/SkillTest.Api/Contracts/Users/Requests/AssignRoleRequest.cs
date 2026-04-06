using SkillTest.Application.Users.Enums;

namespace SkillTest.Api.Contracts.Users.Requests;

public sealed record AssignRoleRequest(UserRole Role);