﻿// <auto-generated />
using System;
using Bookstore.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bookstore.Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241112114549_AddImage")]
    partial class AddImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bookstore.Server.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");

                    b.HasDiscriminator().HasValue("Item");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Book", b =>
                {
                    b.HasBaseType("Bookstore.Server.Data.Models.Item");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId1")
                        .HasColumnType("int");

                    b.HasIndex("CategoryId1");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Magazine", b =>
                {
                    b.HasBaseType("Bookstore.Server.Data.Models.Item");

                    b.Property<int?>("CategoryId1")
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasIndex("CategoryId1");

                    b.ToTable("Items", t =>
                        {
                            t.Property("CategoryId1")
                                .HasColumnName("Magazine_CategoryId1");
                        });

                    b.HasDiscriminator().HasValue("Magazine");
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Item", b =>
                {
                    b.HasOne("Bookstore.Server.Data.Models.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Book", b =>
                {
                    b.HasOne("Bookstore.Server.Data.Models.Category", null)
                        .WithMany("Books")
                        .HasForeignKey("CategoryId1");
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Magazine", b =>
                {
                    b.HasOne("Bookstore.Server.Data.Models.Category", null)
                        .WithMany("Magazines")
                        .HasForeignKey("CategoryId1");
                });

            modelBuilder.Entity("Bookstore.Server.Data.Models.Category", b =>
                {
                    b.Navigation("Books");

                    b.Navigation("Items");

                    b.Navigation("Magazines");
                });
#pragma warning restore 612, 618
        }
    }
}
