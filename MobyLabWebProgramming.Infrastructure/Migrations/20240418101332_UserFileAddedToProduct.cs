using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class UserFileAddedToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFile_User_UserId",
                table: "UserFile");

            migrationBuilder.DropIndex(
                name: "IX_UserFile_ProductId",
                table: "UserFile");

            migrationBuilder.DropIndex(
                name: "IX_UserFile_UserId",
                table: "UserFile");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFile");

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_ProductId",
                table: "UserFile",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFile_ProductId",
                table: "UserFile");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserFile",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_ProductId",
                table: "UserFile",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_UserId",
                table: "UserFile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFile_User_UserId",
                table: "UserFile",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
