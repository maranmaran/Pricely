using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FolderFilesService.Domain.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"), "File_Root", null });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"), "Folder_A", null });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"), "File_A", new Guid("4638a704-604e-4ebf-8da4-2271be9a953d") });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe"), "Folder_B", new Guid("4638a704-604e-4ebf-8da4-2271be9a953d") });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"), "File_B", new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe") });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"), "Folder_C", new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe") });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Name", "ParentFolderId" },
                values: new object[] { new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"), "File_C", new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"));

            migrationBuilder.DeleteData(
                table: "Files",
                keyColumn: "Id",
                keyValue: new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe"));

            migrationBuilder.DeleteData(
                table: "Folders",
                keyColumn: "Id",
                keyValue: new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"));
        }
    }
}
