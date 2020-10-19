using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class PropertyDetailIdAddedToStockPropertyCombination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails");

            migrationBuilder.DropIndex(
                name: "IX_PropertyDetails_StockPropertyCombinationId",
                table: "PropertyDetails");

            migrationBuilder.DropColumn(
                name: "StockPropertyCombinationId",
                table: "PropertyDetails");

            migrationBuilder.AddColumn<int>(
                name: "PropertyDetailId",
                table: "StockPropertyCombinations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockPropertyCombinations_PropertyDetailId",
                table: "StockPropertyCombinations",
                column: "PropertyDetailId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPropertyCombinations_PropertyDetails_PropertyDetailId",
                table: "StockPropertyCombinations",
                column: "PropertyDetailId",
                principalTable: "PropertyDetails",
                principalColumn: "PropertyDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPropertyCombinations_PropertyDetails_PropertyDetailId",
                table: "StockPropertyCombinations");

            migrationBuilder.DropIndex(
                name: "IX_StockPropertyCombinations_PropertyDetailId",
                table: "StockPropertyCombinations");

            migrationBuilder.DropColumn(
                name: "PropertyDetailId",
                table: "StockPropertyCombinations");

            migrationBuilder.AddColumn<int>(
                name: "StockPropertyCombinationId",
                table: "PropertyDetails",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_StockPropertyCombinationId",
                table: "PropertyDetails",
                column: "StockPropertyCombinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails",
                column: "StockPropertyCombinationId",
                principalTable: "StockPropertyCombinations",
                principalColumn: "StockPropertyCombinationId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
