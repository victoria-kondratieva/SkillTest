using SkillTest.Api.Extensions;
using SkillTest.Application;
using SkillTest.Infrastructure.Persistence;
using SkillTest.Infrastructure.Identity;
using SkillTest.Infrastructure.Identity.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);

builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddJwtBearerAuthentication(builder.Configuration);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

await app.SeedIdentityAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();