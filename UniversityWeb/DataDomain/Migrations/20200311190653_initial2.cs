using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataDomain.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalsBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentalsBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BooksId = table.Column<int>(type: "int", nullable: true),
                    RentedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalsBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentalsBooks_Books",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalsBooks_AspNetUsers",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentalsBooks_BooksId",
                table: "RentalsBooks",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalsBooks_UserId",
                table: "RentalsBooks",
                column: "UserId");
        }
    }
}
