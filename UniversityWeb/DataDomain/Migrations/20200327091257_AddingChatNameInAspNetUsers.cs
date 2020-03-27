using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Domain.Migrations
{
    public partial class AddingChatNameInAspNetUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatName",
                table: "AspNetUsers");
        }
    }
}
