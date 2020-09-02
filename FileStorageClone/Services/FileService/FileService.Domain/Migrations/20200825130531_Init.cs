using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemService.Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergen",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(maxLength: 1000, nullable: true),
                    PicturePath = table.Column<string>(maxLength: 250, nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Name = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemAllergen",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    ItemId = table.Column<Guid>(nullable: false),
                    AllergenId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAllergen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemAllergen_Allergen_AllergenId",
                        column: x => x.AllergenId,
                        principalTable: "Allergen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemAllergen_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DateModified = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    ItemId = table.Column<Guid>(nullable: false),
                    IngredientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemIngredient_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Category_ItemId",
                table: "Category",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemAllergen_AllergenId",
                table: "ItemAllergen",
                column: "AllergenId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAllergen_ItemId",
                table: "ItemAllergen",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredient_IngredientId",
                table: "ItemIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredient_ItemId",
                table: "ItemIngredient",
                column: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ItemAllergen");

            migrationBuilder.DropTable(
                name: "ItemIngredient");

            migrationBuilder.DropTable(
                name: "Allergen");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Item");
        }
    }
}
