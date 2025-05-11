using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "core",
                table: "Meal",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_UserId",
                schema: "core",
                table: "Meal",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meal_User_UserId",
                schema: "core",
                table: "Meal",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meal_User_UserId",
                schema: "core",
                table: "Meal");

            migrationBuilder.DropIndex(
                name: "IX_Meal_UserId",
                schema: "core",
                table: "Meal");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "core",
                table: "Meal");
        }
    }
}
