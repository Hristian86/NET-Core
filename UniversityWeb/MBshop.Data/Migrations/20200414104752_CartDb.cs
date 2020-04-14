using Microsoft.EntityFrameworkCore.Migrations;

namespace MBshop.Data.Migrations
{
    public partial class CartDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_Books_BooksId",
                table: "rating");

            migrationBuilder.DropForeignKey(
                name: "FK_rating_Movies_MoviesId",
                table: "rating");

            migrationBuilder.DropForeignKey(
                name: "FK_rating_AspNetUsers_UserId",
                table: "rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rating",
                table: "rating");

            migrationBuilder.RenameTable(
                name: "rating",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_rating_UserId",
                table: "Rating",
                newName: "IX_Rating_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_rating_MoviesId",
                table: "Rating",
                newName: "IX_Rating_MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_rating_BooksId",
                table: "Rating",
                newName: "IX_Rating_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(maxLength: 20, nullable: true),
                    Genre = table.Column<string>(maxLength: 50, nullable: true),
                    Rate = table.Column<double>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    price = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    MovieId = table.Column<int>(nullable: true),
                    BookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_BookId",
                table: "Cart",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_MovieId",
                table: "Cart",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Books_BooksId",
                table: "Rating",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Movies_MoviesId",
                table: "Rating",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Books_BooksId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Movies_MoviesId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_AspNetUsers_UserId",
                table: "Rating");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "rating");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserId",
                table: "rating",
                newName: "IX_rating_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_MoviesId",
                table: "rating",
                newName: "IX_rating_MoviesId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_BooksId",
                table: "rating",
                newName: "IX_rating_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rating",
                table: "rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_Books_BooksId",
                table: "rating",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rating_Movies_MoviesId",
                table: "rating",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_rating_AspNetUsers_UserId",
                table: "rating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
