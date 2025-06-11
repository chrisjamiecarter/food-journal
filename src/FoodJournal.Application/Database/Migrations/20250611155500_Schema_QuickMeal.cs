using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodJournal.BlazorApp.Migrations
{
    /// <inheritdoc />
    public partial class Schema_QuickMeal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuickMeal",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuickMeal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuickMeal_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodQuickMeal",
                schema: "core",
                columns: table => new
                {
                    FoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuickMealsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodQuickMeal", x => new { x.FoodsId, x.QuickMealsId });
                    table.ForeignKey(
                        name: "FK_FoodQuickMeal_Food_FoodsId",
                        column: x => x.FoodsId,
                        principalSchema: "core",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodQuickMeal_QuickMeal_QuickMealsId",
                        column: x => x.QuickMealsId,
                        principalSchema: "core",
                        principalTable: "QuickMeal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodQuickMeal_QuickMealsId",
                schema: "core",
                table: "FoodQuickMeal",
                column: "QuickMealsId");

            migrationBuilder.CreateIndex(
                name: "IX_QuickMeal_UserId",
                schema: "core",
                table: "QuickMeal",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodQuickMeal",
                schema: "core");

            migrationBuilder.DropTable(
                name: "QuickMeal",
                schema: "core");
        }
    }
}
