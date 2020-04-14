using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class CartProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Books_BookId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Movies_MovieId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_BookId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_MovieId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Cart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_BookId",
                table: "Cart",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_MovieId",
                table: "Cart",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Books_BookId",
                table: "Cart",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Movies_MovieId",
                table: "Cart",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
