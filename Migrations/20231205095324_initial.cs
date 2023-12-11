using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSTask.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProductDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ManufacturePhone = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ManufactureEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsAvailable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ManufactureEmail",
                table: "Product",
                column: "ManufactureEmail",
                unique: true,
                filter: "[ManufactureEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductDate",
                table: "Product",
                column: "ProductDate",
                unique: true,
                filter: "[ProductDate] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
