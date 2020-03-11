using Microsoft.EntityFrameworkCore.Migrations;

namespace DataDomain.Migrations
{
    public partial class AddingWebRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "WebUser",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "WebUser",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "WebUser",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WebRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebRole", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebRole");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "WebUser");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "WebUser");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "WebUser");
        }
    }
}
