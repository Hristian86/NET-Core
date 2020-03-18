using Microsoft.EntityFrameworkCore.Migrations;

namespace DataDomain.Migrations
{
    public partial class AddingRaitingCuzIforgotIt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Raiting",
                table: "Movies",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Raiting",
                table: "Movies");
        }
    }
}
