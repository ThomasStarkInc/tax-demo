using System;

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
            name: "Municipalities",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Municipalities", x => x.Id));

        migrationBuilder.CreateTable(
            name: "TaxSchedules",
            schema: "public",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                MunicipalityId = table.Column<Guid>(type: "TEXT", nullable: false),
                TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                StartDateUtc = table.Column<DateTime>(type: "date", nullable: false),
                EndDateUtc = table.Column<DateTime>(type: "date", nullable: false),
                Frequency = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TaxSchedules", x => x.Id);
                table.ForeignKey(
                    name: "FK_TaxSchedules_Municipalities_MunicipalityId",
                    column: x => x.MunicipalityId,
                    principalSchema: "public",
                    principalTable: "Municipalities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_TaxSchedules_MunicipalityId",
            schema: "public",
            table: "TaxSchedules",
            column: "MunicipalityId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TaxSchedules",
            schema: "public");

        migrationBuilder.DropTable(
            name: "Municipalities",
            schema: "public");
    }
}
