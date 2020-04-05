using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class FixingMissplacedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Movies",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
