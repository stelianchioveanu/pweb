using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class AddedConnectionProdUserFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "UserFile",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_ProductId",
                table: "UserFile",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFile_Product_ProductId",
                table: "UserFile",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFile_Product_ProductId",
                table: "UserFile");

            migrationBuilder.DropIndex(
                name: "IX_UserFile_ProductId",
                table: "UserFile");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "UserFile");
        }
    }
}
