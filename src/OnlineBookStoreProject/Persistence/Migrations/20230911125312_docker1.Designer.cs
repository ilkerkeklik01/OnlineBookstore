﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20230911125312_docker1")]
    partial class docker1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Author");

                    b.Property<int?>("BookshelfId")
                        .HasColumnType("int")
                        .HasColumnName("BookshelfId");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryId");

                    b.Property<string>("CoverImagePath")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CoverImagePath");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Discount");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Price");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("PublicationDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("BookshelfId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Bookshelf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bookshelves", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("OrderDate");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TotalPrice");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BookAuthorAtThatTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BookAuthorAtThatTime");

                    b.Property<int>("BookCategoryIdAtThatTime")
                        .HasColumnType("int")
                        .HasColumnName("BookCategoryIdAtThatTime");

                    b.Property<string>("BookCoverImagePathAtThatTime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BookCoverImagePathAtThatTime");

                    b.Property<string>("BookDescriptionAtThatTime")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BookDescriptionAtThatTime");

                    b.Property<decimal>("BookDiscountAtThatTime")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("BookDiscountAtThatTime");

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookId");

                    b.Property<decimal>("BookPriceAtThatTime")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("BookPriceAtThatTime");

                    b.Property<DateTime>("BookPublicationDateAtThatTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("BookPublicationDateAtThatTime");

                    b.Property<string>("BookTitleAtThatTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BookTitleAtThatTime");

                    b.Property<bool>("IsInTheBasket")
                        .HasColumnType("bit");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderId");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("Quantity");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("BookId");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("Rating");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ReviewText");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<DateTime>("PasswordUpdatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("PasswordUpdatedAt");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("RegistrationDate");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.HasOne("Domain.Entities.Bookshelf", "Bookshelf")
                        .WithMany("Books")
                        .HasForeignKey("BookshelfId");

                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bookshelf");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Entities.Bookshelf", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Bookshelves")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("Domain.Entities.Book", "Book")
                        .WithMany("OrderItems")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.HasOne("Domain.Entities.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Book", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Domain.Entities.Bookshelf", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Bookshelves");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
