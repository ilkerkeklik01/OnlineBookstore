using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class OrderItemsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookAuthorAtThatTime",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BookCategoryIdAtThatTime",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BookCoverImagePathAtThatTime",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookDescriptionAtThatTime",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BookDiscountAtThatTime",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BookPriceAtThatTime",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookPublicationDateAtThatTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BookTitleAtThatTime",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookAuthorAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookCategoryIdAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookCoverImagePathAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookDescriptionAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookDiscountAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookPriceAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookPublicationDateAtThatTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "BookTitleAtThatTime",
                table: "OrderItems");
        }
    }
}
