﻿// <auto-generated />
using System;
using Hustex_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hustex_backend.Migrations
{
    [DbContext(typeof(LatexDb))]
    [Migration("20240611142007_InitWebDB_V14")]
    partial class InitWebDB_V14
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Hustex_backend.Models.File", b =>
                {
                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("DataType")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FileName", "ProjectId", "DataType");

                    b.HasIndex("ProjectId");

                    b.ToTable("File");
                });

            modelBuilder.Entity("Hustex_backend.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<DateTime>("LastModified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValue(new DateTime(2024, 6, 11, 21, 20, 4, 295, DateTimeKind.Local).AddTicks(453));

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Hustex_backend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("Hustex_backend.Models.File", b =>
                {
                    b.HasOne("Hustex_backend.Models.Project", "Project")
                        .WithMany("Files")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Hustex_backend.Models.Project", b =>
                {
                    b.HasOne("Hustex_backend.Models.User", "User")
                        .WithMany("Projects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hustex_backend.Models.Project", b =>
                {
                    b.Navigation("Files");
                });

            modelBuilder.Entity("Hustex_backend.Models.User", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
