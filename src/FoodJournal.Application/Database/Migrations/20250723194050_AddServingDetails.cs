using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddServingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                schema: "core",
                table: "Food",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Carbs",
                schema: "core",
                table: "Food",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Fat",
                schema: "core",
                table: "Food",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Protein",
                schema: "core",
                table: "Food",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ServingSize",
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
                name: "Calories",
                schema: "core",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Carbs",
                schema: "core",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Fat",
                schema: "core",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Protein",
                schema: "core",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ServingSize",
                schema: "core",
                table: "Food");
        }
    }
}
