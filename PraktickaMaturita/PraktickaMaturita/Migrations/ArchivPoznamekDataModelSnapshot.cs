﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PraktickaMaturita.Data;

#nullable disable

namespace PraktickaMaturita.Migrations
{
    [DbContext(typeof(ArchivPoznamekData))]
    partial class ArchivPoznamekDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PraktickaMaturita.Models.Poznamka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<string>("Nadpis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Popis")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Vlozeni")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.ToTable("Poznamky");
                });

            modelBuilder.Entity("PraktickaMaturita.Models.Uzivatel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Heslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Jmeno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Uzivatele");
                });

            modelBuilder.Entity("PraktickaMaturita.Models.Poznamka", b =>
                {
                    b.HasOne("PraktickaMaturita.Models.Uzivatel", "Autor")
                        .WithMany("Poznamky")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");
                });

            modelBuilder.Entity("PraktickaMaturita.Models.Uzivatel", b =>
                {
                    b.Navigation("Poznamky");
                });
#pragma warning restore 612, 618
        }
    }
}
