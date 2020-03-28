using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Domain.Migrations
{
    public partial class RateChangesDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingForBooks",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "RatingForMovies",
                table: "rating");

            migrationBuilder.AddColumn<double>(
                name: "RatingBooks",
                table: "rating",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RatingMovies",
                table: "rating",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingBooks",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "RatingMovies",
                table: "rating");

            migrationBuilder.AddColumn<double>(
                name: "RatingForBooks",
                table: "rating",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RatingForMovies",
                table: "rating",
                type: "float",
                nullable: true);
        }
    }
}
