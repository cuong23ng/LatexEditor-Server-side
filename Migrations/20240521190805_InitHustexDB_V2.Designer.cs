﻿// <auto-generated />
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
    [Migration("20240521190805_InitHustexDB_V2")]
    partial class InitHustexDB_V2
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
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FileId"));

                    b.Property<string>("Data_type")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("FileId");

                    b.HasIndex("ProjectId");

                    b.ToTable("File");

                    b.HasDiscriminator<string>("Data_type").HasValue("NoFormat");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Hustex_backend.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ProjectId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Hustex_backend.Models.OtherFile", b =>
                {
                    b.HasBaseType("Hustex_backend.Models.File");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("File");

                    b.HasDiscriminator().HasValue("Other");
                });

            modelBuilder.Entity("Hustex_backend.Models.TexFile", b =>
                {
                    b.HasBaseType("Hustex_backend.Models.File");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("File");

                    b.HasDiscriminator().HasValue("Latex");
                });

            modelBuilder.Entity("Hustex_backend.Models.File", b =>
                {
                    b.HasOne("Hustex_backend.Models.Project", "project")
                        .WithMany("Files")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("project");
                });

            modelBuilder.Entity("Hustex_backend.Models.Project", b =>
                {
                    b.Navigation("Files");
                });
#pragma warning restore 612, 618
        }
    }
}
