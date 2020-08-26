using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityService.Domain.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Allergens",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"), "Found in bread.", "Gluten" },
                    { new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe"), "Found in milk.", "Lactose" },
                    { new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"), "Belongs to fruits food group.", "Orange" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("9a542d51-7aa0-488e-87f2-9aef980680cb"), null, "Food" },
                    { new Guid("ce29cfd1-f374-4504-9358-a137ffa1c2c7"), null, "Non-alcoholic beverages" },
                    { new Guid("42e774af-4372-4ccb-aee7-d8f7b11a9b28"), null, "Alcoholic beverages" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"), "Fine hand made spaghetti.", "Spaghetti" },
                    { new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"), "Best home grown tomato.", "Tomato" },
                    { new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"), null, "Tequilla" },
                    { new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"), null, "Juice" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Active", "CategoryId", "Description", "Name", "PicturePath", "PictureUrl" },
                values: new object[] { new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614"), false, new Guid("9a542d51-7aa0-488e-87f2-9aef980680cb"), "Made in our kitchen", "Spaghetti bolognese", null, "https://images.unsplash.com/photo-1572441713132-c542fc4fe282?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=700&q=80" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Active", "CategoryId", "Description", "Name", "PicturePath", "PictureUrl" },
                values: new object[] { new Guid("11c635ed-186e-4c2a-a7a7-1ef9e991d463"), false, new Guid("ce29cfd1-f374-4504-9358-a137ffa1c2c7"), "Worldwide drink", "Coca-cola", null, "https://images.unsplash.com/photo-1561758033-48d52648ae8b?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=634&q=80" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Active", "CategoryId", "Description", "Name", "PicturePath", "PictureUrl" },
                values: new object[] { new Guid("d3b56d57-453c-4382-9e49-437022e47f2a"), false, new Guid("42e774af-4372-4ccb-aee7-d8f7b11a9b28"), "Refreshing cocktail", "Tequilla sunrise", null, "https://images.unsplash.com/photo-1558645836-e44122a743ee?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=634&q=80" });

            migrationBuilder.InsertData(
                table: "ItemAllergens",
                columns: new[] { "AllergenId", "ItemId" },
                values: new object[,]
                {
                    { new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"), new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614") },
                    { new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"), new Guid("d3b56d57-453c-4382-9e49-437022e47f2a") }
                });

            migrationBuilder.InsertData(
                table: "ItemIngredients",
                columns: new[] { "IngredientId", "ItemId" },
                values: new object[,]
                {
                    { new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"), new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614") },
                    { new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"), new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614") },
                    { new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"), new Guid("d3b56d57-453c-4382-9e49-437022e47f2a") },
                    { new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"), new Guid("d3b56d57-453c-4382-9e49-437022e47f2a") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "Id",
                keyValue: new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe"));

            migrationBuilder.DeleteData(
                table: "ItemAllergens",
                keyColumns: new[] { "AllergenId", "ItemId" },
                keyValues: new object[] { new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"), new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614") });

            migrationBuilder.DeleteData(
                table: "ItemAllergens",
                keyColumns: new[] { "AllergenId", "ItemId" },
                keyValues: new object[] { new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"), new Guid("d3b56d57-453c-4382-9e49-437022e47f2a") });

            migrationBuilder.DeleteData(
                table: "ItemIngredients",
                keyColumns: new[] { "IngredientId", "ItemId" },
                keyValues: new object[] { new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"), new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614") });

            migrationBuilder.DeleteData(
                table: "ItemIngredients",
                keyColumns: new[] { "IngredientId", "ItemId" },
                keyValues: new object[] { new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"), new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614") });

            migrationBuilder.DeleteData(
                table: "ItemIngredients",
                keyColumns: new[] { "IngredientId", "ItemId" },
                keyValues: new object[] { new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"), new Guid("d3b56d57-453c-4382-9e49-437022e47f2a") });

            migrationBuilder.DeleteData(
                table: "ItemIngredients",
                keyColumns: new[] { "IngredientId", "ItemId" },
                keyValues: new object[] { new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"), new Guid("d3b56d57-453c-4382-9e49-437022e47f2a") });

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("11c635ed-186e-4c2a-a7a7-1ef9e991d463"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "Id",
                keyValue: new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"));

            migrationBuilder.DeleteData(
                table: "Allergens",
                keyColumn: "Id",
                keyValue: new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ce29cfd1-f374-4504-9358-a137ffa1c2c7"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"));

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("d3b56d57-453c-4382-9e49-437022e47f2a"));

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: new Guid("dbd5bd9c-f317-4b16-8e37-8b18bf07d614"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("42e774af-4372-4ccb-aee7-d8f7b11a9b28"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9a542d51-7aa0-488e-87f2-9aef980680cb"));
        }
    }
}
