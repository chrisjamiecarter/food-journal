using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class Food_Base64Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Image",
                schema: "core",
                table: "Food",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Image",
                schema: "core",
                table: "Food");
        }
    }
}
