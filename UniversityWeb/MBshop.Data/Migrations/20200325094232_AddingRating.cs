using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class AddingRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rating",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRata = table.Column<int>(nullable: false),
                    FinalRatingForMovies = table.Column<double>(nullable: false),
                    FinalRatingForBooks = table.Column<double>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MoviesId = table.Column<int>(nullable: true),
                    BooksId = table.Column<int>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rating_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rating_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rating_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rating_BooksId",
                table: "rating",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_MoviesId",
                table: "rating",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_rating_UserId1",
                table: "rating",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rating");
        }
    }
}
