using SkillTest.Domain.Tests.Entities;

namespace SkillTest.Application.Common.Mappers;

public static class TestMapper
{
    public static object ToResponse(Test t) => new
    {
        id = t.Id.Value,
        name = t.Name,
        description = t.Description,
        duration = t.Duration,
        status = t.Status.Value,
        maxScore = t.MaxScore,
        difficultyLevel = t.DifficultyLevel.Value,

        category = new
        {
            id = t.CategoryId.Value,
            name = t.Category?.Name
        },

        createdBy = t.CreatedBy.Value,
        createdAt = t.CreatedAt,
        updatedAt = t.UpdatedAt,

        questions = t.Questions
            .OrderBy(q => q.OrderIndex)
            .Select(q => new
            {
                id = q.Id.Value,
                text = q.Text,
                type = q.Type.Value,
                points = q.Points,
                orderIndex = q.OrderIndex,

                answers = q.Answers
                    .OrderBy(a => a.OrderIndex)
                    .Select(a => new
                    {
                        id = a.Id.Value,
                        text = a.Text,
                        isCorrect = a.IsCorrect,
                        orderIndex = a.OrderIndex
                    }),

                tags = t.Tags.Select(tag => new
                {
                    name = tag.Value
                })
            }),

        tags = t.Tags.Select(tag => new
        {
            name = tag.Value
        })
    };
}
