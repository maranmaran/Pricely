﻿// <auto-generated />
using System;
using FolderFilesService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FolderFilesService.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200902112505_Seed")]
    partial class Seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FolderFilesService.Domain.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("ParentFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Files");

                    b.HasData(
                        new
                        {
                            Id = new Guid("daff8847-a21b-45ec-a3d4-25b1c585d842"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Root"
                        },
                        new
                        {
                            Id = new Guid("0e2f0009-f8ac-4dbd-961a-b89adedec3c7"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Folder_A",
                            ParentFolderId = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("c272feac-ac31-4a09-b73c-0d3305ba721c"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Subfolder_A",
                            ParentFolderId = new Guid("02e84ea4-3480-478f-ae03-2760935b19ac")
                        },
                        new
                        {
                            Id = new Guid("4aaeeca1-a5bd-409a-8722-0b904e2307a4"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Subfolder_Subfolder_A",
                            ParentFolderId = new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26")
                        },
                        new
                        {
                            Id = new Guid("1e2f0009-f8ac-4dbd-961a-b89adedec3c7"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Folder_B",
                            ParentFolderId = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("d272feac-ac31-4a09-b73c-0d3305ba721c"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Subfolder_B",
                            ParentFolderId = new Guid("12e84ea4-3480-478f-ae03-2760935b19ac")
                        },
                        new
                        {
                            Id = new Guid("5aaeeca1-a5bd-409a-8722-0b904e2307a4"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Subfolder_Subfolder_B",
                            ParentFolderId = new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26")
                        },
                        new
                        {
                            Id = new Guid("2e2f0009-f8ac-4dbd-961a-b89adedec3c7"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Folder_C",
                            ParentFolderId = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("e272feac-ac31-4a09-b73c-0d3305ba721c"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Subfolder_C",
                            ParentFolderId = new Guid("22e84ea4-3480-478f-ae03-2760935b19ac")
                        },
                        new
                        {
                            Id = new Guid("6aaeeca1-a5bd-409a-8722-0b904e2307a4"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "File_Subfolder_Subfolder_C",
                            ParentFolderId = new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26")
                        });
                });

            modelBuilder.Entity("FolderFilesService.Domain.Entities.Folder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<DateTime>("DateModified")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<Guid?>("ParentFolderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Folders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Folder_A"
                        },
                        new
                        {
                            Id = new Guid("02e84ea4-3480-478f-ae03-2760935b19ac"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_A_1",
                            ParentFolderId = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("0344fc6c-81d7-4808-91b4-66dd7f8fef26"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_Subfolder_A_1",
                            ParentFolderId = new Guid("02e84ea4-3480-478f-ae03-2760935b19ac")
                        },
                        new
                        {
                            Id = new Guid("042d9c67-1f99-4cf0-a307-c91f74896905"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_A_2",
                            ParentFolderId = new Guid("0138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Folder_B"
                        },
                        new
                        {
                            Id = new Guid("12e84ea4-3480-478f-ae03-2760935b19ac"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_B_1",
                            ParentFolderId = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("1344fc6c-81d7-4808-91b4-66dd7f8fef26"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_Subfolder_B_1",
                            ParentFolderId = new Guid("12e84ea4-3480-478f-ae03-2760935b19ac")
                        },
                        new
                        {
                            Id = new Guid("142d9c67-1f99-4cf0-a307-c91f74896905"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_B_2",
                            ParentFolderId = new Guid("1138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Folder_C"
                        },
                        new
                        {
                            Id = new Guid("22e84ea4-3480-478f-ae03-2760935b19ac"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_C_1",
                            ParentFolderId = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d")
                        },
                        new
                        {
                            Id = new Guid("2344fc6c-81d7-4808-91b4-66dd7f8fef26"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_Subfolder_C_1",
                            ParentFolderId = new Guid("22e84ea4-3480-478f-ae03-2760935b19ac")
                        },
                        new
                        {
                            Id = new Guid("242d9c67-1f99-4cf0-a307-c91f74896905"),
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DateModified = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Subfolder_C_2",
                            ParentFolderId = new Guid("2138a704-604e-4ebf-8da4-2271be9a953d")
                        });
                });

            modelBuilder.Entity("FolderFilesService.Domain.Entities.File", b =>
                {
                    b.HasOne("FolderFilesService.Domain.Entities.Folder", "ParentFolder")
                        .WithMany("Files")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FolderFilesService.Domain.Entities.Folder", b =>
                {
                    b.HasOne("FolderFilesService.Domain.Entities.Folder", "ParentFolder")
                        .WithMany("Folders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
