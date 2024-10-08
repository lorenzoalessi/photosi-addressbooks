﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhotosiAddressBooks.Model;

#nullable disable

namespace PhotosiAddressBooks.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240813105256_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PhotosiAddressBooks.Model.AddressBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_name");

                    b.Property<string>("AddressNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("address_number");

                    b.Property<string>("Cap")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("cap");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city_name");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country_name");

                    b.HasKey("Id");

                    b.HasIndex("AddressName", "AddressNumber", "Cap", "CityName", "CountryName")
                        .IsUnique();

                    b.ToTable("address_book");
                });
#pragma warning restore 612, 618
        }
    }
}
