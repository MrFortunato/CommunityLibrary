﻿// <auto-generated />
using System;
using CommunityLibrary.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CommunityLibrary.Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250112165148_dbInicial")]
    partial class dbInicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CommunityLibrary.Domain.Author", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("RegisteredByUserId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("Status")
                        .HasColumnType("TINYINT(1)");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("BINARY(16)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Book", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<byte[]>("AuthorId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<byte[]>("BookCategoryId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("PublishedDate")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("RegisteredByUserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<bool>("Status")
                        .HasColumnType("TINYINT(1)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BookCategoryId");

                    b.HasIndex("RegisteredByUserId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.BookCategory", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<byte[]>("RegisteredByUserId")
                        .IsRequired()
                        .HasColumnType("BINARY(16)");

                    b.Property<bool>("Status")
                        .HasColumnType("TINYINT(1)");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredByUserId");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.BookRental", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<byte[]>("BookId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<byte[]>("ClientId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("RentalDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("Returned")
                        .HasColumnType("TINYINT(1)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ClientId");

                    b.HasIndex("UserId");

                    b.ToTable("BookRentals");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Client", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("Status")
                        .HasColumnType("TINYINT(1)");

                    b.Property<byte[]>("UserId")
                        .IsRequired()
                        .HasColumnType("BINARY(16)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.User", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BINARY(16)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("TINYINT(1)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Author", b =>
                {
                    b.HasOne("CommunityLibrary.Domain.User", "User")
                        .WithMany("RegisteredAuthors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Book", b =>
                {
                    b.HasOne("CommunityLibrary.Domain.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CommunityLibrary.Domain.BookCategory", "BookCategory")
                        .WithMany("Books")
                        .HasForeignKey("BookCategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CommunityLibrary.Domain.User", "RegisteredUser")
                        .WithMany("RegisteredBooks")
                        .HasForeignKey("RegisteredByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("BookCategory");

                    b.Navigation("RegisteredUser");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.BookCategory", b =>
                {
                    b.HasOne("CommunityLibrary.Domain.User", "RegisteredUser")
                        .WithMany("RegisteredBookCategories")
                        .HasForeignKey("RegisteredByUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RegisteredUser");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.BookRental", b =>
                {
                    b.HasOne("CommunityLibrary.Domain.Book", "Book")
                        .WithMany("BookRentals")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CommunityLibrary.Domain.Client", "Client")
                        .WithMany("BookRentals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CommunityLibrary.Domain.User", "User")
                        .WithMany("BookRentals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Client");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Client", b =>
                {
                    b.HasOne("CommunityLibrary.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Book", b =>
                {
                    b.Navigation("BookRentals");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.BookCategory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.Client", b =>
                {
                    b.Navigation("BookRentals");
                });

            modelBuilder.Entity("CommunityLibrary.Domain.User", b =>
                {
                    b.Navigation("BookRentals");

                    b.Navigation("RegisteredAuthors");

                    b.Navigation("RegisteredBookCategories");

                    b.Navigation("RegisteredBooks");
                });
#pragma warning restore 612, 618
        }
    }
}
