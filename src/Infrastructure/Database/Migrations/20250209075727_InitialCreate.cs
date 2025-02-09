using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Email = table.Column<string>(type: "TEXT", nullable: false),
                FirstName = table.Column<string>(type: "TEXT", nullable: false),
                LastName = table.Column<string>(type: "TEXT", nullable: false),
                PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Users", x => x.Id));

        migrationBuilder.CreateTable(
            name: "TodoItems",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                Description = table.Column<string>(type: "TEXT", nullable: false),
                DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                Labels = table.Column<string>(type: "TEXT", nullable: false),
                IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                Priority = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TodoItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_TodoItems_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "public",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_TodoItems_UserId",
            schema: "public",
            table: "TodoItems",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            schema: "public",
            table: "Users",
            column: "Email",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TodoItems",
            schema: "public");

        migrationBuilder.DropTable(
            name: "Users",
            schema: "public");
    }
}
