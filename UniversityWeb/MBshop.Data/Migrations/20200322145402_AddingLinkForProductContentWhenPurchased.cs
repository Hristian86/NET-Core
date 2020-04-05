using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class AddingLinkForProductContentWhenPurchased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductContentWhenPurchase",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ProductContentWhenPurchase",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "LinkForProductContentWhenPurchase",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkForProductContentWhenPurchase",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkForProductContentWhenPurchase",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "LinkForProductContentWhenPurchase",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "ProductContentWhenPurchase",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductContentWhenPurchase",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
