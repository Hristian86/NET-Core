using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class MakingRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalRatingForBooks",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "FinalRatingForMovies",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "UserRata",
                table: "rating");

            migrationBuilder.AddColumn<double>(
                name: "RatingForBooks",
                table: "rating",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RatingForMovies",
                table: "rating",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UserRateCount",
                table: "rating",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingForBooks",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "RatingForMovies",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "UserRateCount",
                table: "rating");

            migrationBuilder.AddColumn<double>(
                name: "FinalRatingForBooks",
                table: "rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FinalRatingForMovies",
                table: "rating",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UserRata",
                table: "rating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
