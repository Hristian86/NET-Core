using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Domain.Migrations
{
    public partial class MakingRatingAnota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_AspNetUsers_UserId1",
                table: "rating");

            migrationBuilder.DropIndex(
                name: "IX_rating_UserId1",
                table: "rating");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "rating");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "rating",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_rating_UserId",
                table: "rating",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_AspNetUsers_UserId",
                table: "rating",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rating_AspNetUsers_UserId",
                table: "rating");

            migrationBuilder.DropIndex(
                name: "IX_rating_UserId",
                table: "rating");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "rating",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "rating",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_rating_UserId1",
                table: "rating",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_rating_AspNetUsers_UserId1",
                table: "rating",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
