using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class sixth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OrderItemId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderItemId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_BookId",
                table: "OrderItems",
                column: "BookId",
                unique: true);
        }
    }
}
