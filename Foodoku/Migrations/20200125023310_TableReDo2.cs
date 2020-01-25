using Microsoft.EntityFrameworkCore.Migrations;

namespace Foodoku.Migrations
{
    public partial class TableReDo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Recipes",
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
                    table.PrimaryKey("PK_Recipes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurements",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
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
                    UnitOfMeasurementID = table.Column<int>(nullable: true),
                    IsInPantry = table.Column<bool>(nullable: true),
                    GroceryNote = table.Column<string>(nullable: true),
                    LocationID = table.Column<int>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    UnitOfMeasurementID1 = table.Column<int>(nullable: true)
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
                        name: "FK_FoodItem_UnitOfMeasurements_UnitOfMeasurementID",
                        column: x => x.UnitOfMeasurementID,
                        principalTable: "UnitOfMeasurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoodItem_Locations_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Locations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItem_UnitOfMeasurements_UnitOfMeasurementID1",
                        column: x => x.UnitOfMeasurementID1,
                        principalTable: "UnitOfMeasurements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    IngredientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.RecipeID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_FoodItem_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "FoodItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_GroceryItemLocationID",
                table: "FoodItem",
                column: "GroceryItemLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_UnitOfMeasurementID",
                table: "FoodItem",
                column: "UnitOfMeasurementID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_LocationID",
                table: "FoodItem",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItem_UnitOfMeasurementID1",
                table: "FoodItem",
                column: "UnitOfMeasurementID1");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientID",
                table: "RecipeIngredients",
                column: "IngredientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FoodItem");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurements");
        }
    }
}
