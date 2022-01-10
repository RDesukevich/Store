using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class ApplicationTypeProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationTypeId",
                table: "Product",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Product_ApplicationTypeId",
                table: "Product",
                column: "ApplicationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeId",
                table: "Product",
                column: "ApplicationTypeId",
                principalTable: "ApplicationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ApplicationType_ApplicationTypeId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ApplicationTypeId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ApplicationTypeId",
                table: "Product");
        }
    }
}
