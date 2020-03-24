using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Domain.Migrations
{
    public partial class AddingProductContentWhenPurchased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductContentWhenPurchase",
                table: "Movies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductContentWhenPurchase",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductContentWhenPurchase",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ProductContentWhenPurchase",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Books");
        }
    }
}
