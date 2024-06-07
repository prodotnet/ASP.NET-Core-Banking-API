﻿// <auto-generated />
using System;
using BankingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankingApi.Models.ClientBankDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Accounttype")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ClientDetailsId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientDetailsId");

                    b.ToTable("ClientBankDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountNumber = "1234567890",
                            Accounttype = 0,
                            Balance = 1000m,
                            ClientDetailsId = 1,
                            Name = "Sphe Ngidi Cheque",
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            AccountNumber = "9876543210",
                            Accounttype = 1,
                            Balance = 500m,
                            ClientDetailsId = 2,
                            Name = "Pro Dot Savings",
                            Status = 0
                        },
                        new
                        {
                            Id = 3,
                            AccountNumber = "1376543210",
                            Accounttype = 2,
                            Balance = 200m,
                            ClientDetailsId = 3,
                            Name = "Sphesihle Smith FixedDeposit",
                            Status = 0
                        });
                });

            modelBuilder.Entity("BankingApi.Models.ClientDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ClientDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "1 First st Johannesburg",
                            ClientID = "0123456789012",
                            EmailAddress = "sphe@gmail.com",
                            Gender = 0,
                            MobileNumber = "07123456789",
                            Name = "Sphe",
                            Surname = "Ngidi"
                        },
                        new
                        {
                            Id = 2,
                            Address = "2 Second st Johannesburg",
                            ClientID = "2123456789012",
                            EmailAddress = "ProD@gmail.com",
                            Gender = 1,
                            MobileNumber = "07234567890",
                            Name = "Pro",
                            Surname = "Dot"
                        },
                        new
                        {
                            Id = 3,
                            Address = "23 Second st Johannesburg",
                            ClientID = "3123456789012",
                            EmailAddress = "sphesihle1@gmail.com",
                            Gender = 0,
                            MobileNumber = "0734567890",
                            Name = "Sphesihle",
                            Surname = "Smith"
                        });
                });

            modelBuilder.Entity("BankingApi.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Changes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingApi.Models.ClientBankDetail", b =>
                {
                    b.HasOne("BankingApi.Models.ClientDetail", "ClientDetails")
                        .WithMany("ClientBankDetails")
                        .HasForeignKey("ClientDetailsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientDetails");
                });

            modelBuilder.Entity("BankingApi.Models.ClientDetail", b =>
                {
                    b.Navigation("ClientBankDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
