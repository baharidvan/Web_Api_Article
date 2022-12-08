﻿// <auto-generated />
using Books.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Books.WebApi.Migrations
{
    [DbContext(typeof(ArticleContext))]
    partial class ArticleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Books.WebApi.Data.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Doğan cüceloğlu",
                            Name = "Savaşçı",
                            Price = 100,
                            Publisher = "Timaş"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Can Yılmaz",
                            Name = "Osmanlı Tarihi",
                            Price = 120,
                            Publisher = "Can"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Tolstoy",
                            Name = "Savaş ve Barış",
                            Price = 140,
                            Publisher = "Zeren"
                        },
                        new
                        {
                            Id = 4,
                            Author = "Ahmet Akpınar",
                            Name = "Kanlı Elmas",
                            Price = 200,
                            Publisher = "Parıltı"
                        },
                        new
                        {
                            Id = 5,
                            Author = "Volkan Kırat",
                            Name = "Muhteşem İstanbul",
                            Price = 125,
                            Publisher = "Levent"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
