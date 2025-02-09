using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class AddingIgnoreOnTaxScheduleMunicipality : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameTable(
            name: "TaxSchedules",
            schema: "public",
            newName: "TaxSchedules");

        migrationBuilder.RenameTable(
            name: "Municipalities",
            schema: "public",
            newName: "Municipalities");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.RenameTable(
            name: "TaxSchedules",
            newName: "TaxSchedules",
            newSchema: "public");

        migrationBuilder.RenameTable(
            name: "Municipalities",
            newName: "Municipalities",
            newSchema: "public");
    }
}
