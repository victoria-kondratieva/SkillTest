using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillTest.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { new Guid("5b9e1f22-3c8d-4f7a-9d11-7c8e2a1b4f99"), "#33FF57", "CI/CD, automation, infrastructure", "devops", "DevOps" },
                    { new Guid("9c7a4e11-2d3f-4b8a-9f55-6e7d8c9b1a44"), "#FFC300", "Frontend, backend, web technologies", "web", "Web Development" },
                    { new Guid("a8d9f3b2-7c44-4c1e-9f0a-2e6b1d9a8f55"), "#FF5733", "All programming-related topics", "code", "Programming" },
                    { new Guid("c1e7a2d4-9b33-4f0d-8c77-1a2b3c4d5e66"), "#33A1FF", "Network technologies and protocols", "network", "Networking" },
                    { new Guid("d4a1c7e8-2b55-4f33-8e0c-9a1d2b3c4e77"), "#FF33A8", "SQL, NoSQL, data modeling", "database", "Databases" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "TotalPoints", "Email", "FirstName", "LastName", "AvatarUrl", "Position", "Username", "AutoAdvanceEnabled", "EmailNotificationsEnabled", "Language", "TimeLimitSeconds" },
                values: new object[] { new Guid("3f4c2c8c-1c0e-4e4e-9b8e-0f7a4b6d2c11"), 0, "admin@example.com", "Admin", "Admin", "", "Administrator", "admin", false, true, "ru", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5b9e1f22-3c8d-4f7a-9d11-7c8e2a1b4f99"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9c7a4e11-2d3f-4b8a-9f55-6e7d8c9b1a44"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a8d9f3b2-7c44-4c1e-9f0a-2e6b1d9a8f55"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1e7a2d4-9b33-4f0d-8c77-1a2b3c4d5e66"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d4a1c7e8-2b55-4f33-8e0c-9a1d2b3c4e77"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3f4c2c8c-1c0e-4e4e-9b8e-0f7a4b6d2c11"));
        }
    }
}
