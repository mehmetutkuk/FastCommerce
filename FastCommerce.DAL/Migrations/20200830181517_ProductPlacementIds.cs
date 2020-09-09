using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class ProductPlacementIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.AddColumn<int[]>(
                name: "PlacementIds",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacementIds",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "ProductID");
        }
    }
}
