using System;
using System.Collections.Generic;
using FolderFilesService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FolderFilesService.Domain.Seed
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.SeedFolders();
            builder.SeedFiles();
        }

        private static void SeedFolders(this ModelBuilder builder)
        {
            var entities = new List<Folder>()
            {
                new Folder()
                {
                    Id = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d"),
                    Name = "Folder_A",
                },
                new Folder()
                {
                    Id = new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe"),
                    Name = "Folder_B",
                    ParentFolderId = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d")
                },
                new Folder()
                {
                    Id = new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe"),
                    Name = "Folder_C",
                    ParentFolderId = new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe")
                }
            };

            builder.Entity<Folder>().HasData(entities);
        }
        private static void SeedFiles(this ModelBuilder builder)
        {
            var entities = new List<File>()
            {
                new File()
                {
                    Id = new Guid("bf7905e0-6a86-44ec-a7c1-4e8e74e76ed5"),
                    Name = "File_A",
                    ParentFolderId = new Guid("4638a704-604e-4ebf-8da4-2271be9a953d")
                },
                new File()
                {
                    Id = new Guid("40f44902-a38c-4617-a870-b7e6b917c31e"),
                    Name = "File_B",
                    ParentFolderId = new Guid("625c60f6-75bf-421f-b681-6a3848ee25fe")
                },
                new File()
                {
                    Id = new Guid("c7205103-c499-4ec2-9a30-2d3e37d8b1ab"),
                    Name = "File_C",
                    ParentFolderId = new Guid("f5a6f6d4-ca85-4467-93bb-fabbdd633dfe")
                },
                new File()
                {
                    Id = new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"),
                    Name = "File_Root",
                }
            };

            builder.Entity<File>().HasData(entities);
        }
    }
}
