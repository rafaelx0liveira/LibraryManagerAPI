﻿// <auto-generated />
using System;
using LibraryManagerAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryManagerAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20241228202038_UpdateLoanStatusToString")]
    partial class UpdateLoanStatusToString
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Author")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Author");

                    b.Property<string>("ISBN")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ISBN");

                    b.Property<string>("Language")
                        .HasColumnType("longtext")
                        .HasColumnName("Language");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<string>("Title")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("Title");

                    b.Property<int>("Year")
                        .HasColumnType("int")
                        .HasColumnName("Year");

                    b.HasKey("Id");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Author = "F. Scott Fitzgerald",
                            ISBN = "9780743273565",
                            Quantity = 3,
                            Title = "The Great Gatsby",
                            Year = 1925
                        },
                        new
                        {
                            Id = 2L,
                            Author = "George Orwell",
                            ISBN = "9780451524935",
                            Quantity = 5,
                            Title = "1984",
                            Year = 1949
                        });
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.Loan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Date");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("ReturnDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Status");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Loan");
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.LoanBook", b =>
                {
                    b.Property<long>("LoanId")
                        .HasColumnType("bigint");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint");

                    b.HasKey("LoanId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("LoanBook");
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Email = "john.doe@example.com",
                            Name = "John Doe"
                        },
                        new
                        {
                            Id = 2L,
                            Email = "jane.smith@example.com",
                            Name = "Jane Smith"
                        });
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.Loan", b =>
                {
                    b.HasOne("LibraryManagerAPI.Domain.Entities.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.LoanBook", b =>
                {
                    b.HasOne("LibraryManagerAPI.Domain.Entities.Book", "Book")
                        .WithMany("LoanBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryManagerAPI.Domain.Entities.Loan", "Loan")
                        .WithMany("LoanBooks")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.Book", b =>
                {
                    b.Navigation("LoanBooks");
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.Loan", b =>
                {
                    b.Navigation("LoanBooks");
                });

            modelBuilder.Entity("LibraryManagerAPI.Domain.Entities.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
