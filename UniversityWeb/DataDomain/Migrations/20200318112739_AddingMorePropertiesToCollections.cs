using Microsoft.EntityFrameworkCore.Migrations;

namespace DataDomain.Migrations
{
    public partial class AddingMorePropertiesToCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Director",
                table: "Movies",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Movies",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Actors",
                table: "Books",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Raiting",
                table: "Books",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Actors",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Raiting",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Director",
                table: "Movies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
