using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodoku.Migrations.FoodokuDb
{
    public partial class DistinctPantry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(nullable: true),
                    Yield = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Instructions = table.Column<string>(nullable: true),
                    Ingredients = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurement",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FoodItem",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    GroceryItemLocationID = table.Column<int>(nullable: true),
                    UnitOfMeasurementID1 = table.Column<int>(nullable: true),
                    IsInPantry = table.Column<bool>(nullable: true),
                    GroceryNote = table.Column<string>(nullable: true),
                    LocationID = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    UnitOfMeasurementID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FoodItem_Locations_GroceryItemLocationID",
                        column: x => x.GroceryItemLocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodItem_UnitOfMeasurement_UnitOfMeasurementID1",
                        column: x => x.UnitOfMeasurementID1,
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodItem_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItem_IdentityUser_UserId",
                        column: x => x.UserId,
                        principalTable: "IdentityUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodItem_UnitOfMeasurement_UnitOfMeasurementID",
                        column: x => x.UnitOfMeasurementID,
                        principalTable: "UnitOfMeasurement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    IngredientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => new { x.RecipeID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_FoodItem_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "FoodItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_GroceryItemLocationID",
                table: "FoodItem",
                column: "GroceryItemLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_UnitOfMeasurementID1",
                table: "FoodItem",
                column: "UnitOfMeasurementID1");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_LocationID",
                table: "FoodItem",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_UserId",
                table: "FoodItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_UnitOfMeasurementID",
                table: "FoodItem",
                column: "UnitOfMeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientID",
                table: "RecipeIngredient",
                column: "IngredientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "FoodItem");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurement");

            migrationBuilder.DropTable(
                name: "IdentityUser");
        }
    }
}
