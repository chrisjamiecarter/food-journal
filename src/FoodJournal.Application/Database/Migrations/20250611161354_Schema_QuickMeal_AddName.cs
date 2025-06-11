using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class Schema_QuickMeal_AddName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "core",
                table: "QuickMeal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "core",
                table: "QuickMeal");
        }
    }
}
