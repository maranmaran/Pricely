using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemService.Domain.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Item_ItemId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAllergen_Allergen_AllergenId",
                table: "ItemAllergen");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAllergen_Item_ItemId",
                table: "ItemAllergen");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredient_Ingredient_IngredientId",
                table: "ItemIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredient_Item_ItemId",
                table: "ItemIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemIngredient",
                table: "ItemIngredient");

            migrationBuilder.DropIndex(
                name: "IX_ItemIngredient_IngredientId",
                table: "ItemIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemAllergen",
                table: "ItemAllergen");

            migrationBuilder.DropIndex(
                name: "IX_ItemAllergen_AllergenId",
                table: "ItemAllergen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergen",
                table: "Allergen");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemIngredient");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItemAllergen");

            migrationBuilder.RenameTable(
                name: "ItemIngredient",
                newName: "ItemIngredients");

            migrationBuilder.RenameTable(
                name: "ItemAllergen",
                newName: "ItemAllergens");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Allergen",
                newName: "Allergens");

            migrationBuilder.RenameIndex(
                name: "IX_ItemIngredient_ItemId",
                table: "ItemIngredients",
                newName: "IX_ItemIngredients_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemAllergen_ItemId",
                table: "ItemAllergens",
                newName: "IX_ItemAllergens_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ItemId",
                table: "Categories",
                newName: "IX_Categories_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemIngredients",
                table: "ItemIngredients",
                columns: new[] { "IngredientId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemAllergens",
                table: "ItemAllergens",
                columns: new[] { "AllergenId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Items_ItemId",
                table: "Categories",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAllergens_Allergens_AllergenId",
                table: "ItemAllergens",
                column: "AllergenId",
                principalTable: "Allergens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAllergens_Items_ItemId",
                table: "ItemAllergens",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientId",
                table: "ItemIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredients_Items_ItemId",
                table: "ItemIngredients",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Items_ItemId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAllergens_Allergens_AllergenId",
                table: "ItemAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAllergens_Items_ItemId",
                table: "ItemAllergens");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Ingredients_IngredientId",
                table: "ItemIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemIngredients_Items_ItemId",
                table: "ItemIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemIngredients",
                table: "ItemIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemAllergens",
                table: "ItemAllergens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Allergens",
                table: "Allergens");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "ItemIngredients",
                newName: "ItemIngredient");

            migrationBuilder.RenameTable(
                name: "ItemAllergens",
                newName: "ItemAllergen");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Allergens",
                newName: "Allergen");

            migrationBuilder.RenameIndex(
                name: "IX_ItemIngredients_ItemId",
                table: "ItemIngredient",
                newName: "IX_ItemIngredient_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemAllergens_ItemId",
                table: "ItemAllergen",
                newName: "IX_ItemAllergen_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ItemId",
                table: "Category",
                newName: "IX_Category_ItemId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ItemIngredient",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ItemAllergen",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemIngredient",
                table: "ItemIngredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemAllergen",
                table: "ItemAllergen",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allergen",
                table: "Allergen",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ItemIngredient_IngredientId",
                table: "ItemIngredient",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAllergen_AllergenId",
                table: "ItemAllergen",
                column: "AllergenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Item_ItemId",
                table: "Category",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAllergen_Allergen_AllergenId",
                table: "ItemAllergen",
                column: "AllergenId",
                principalTable: "Allergen",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAllergen_Item_ItemId",
                table: "ItemAllergen",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredient_Ingredient_IngredientId",
                table: "ItemIngredient",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemIngredient_Item_ItemId",
                table: "ItemIngredient",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
