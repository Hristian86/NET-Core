using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class RateChangesDo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Books");
        }
    }
}
