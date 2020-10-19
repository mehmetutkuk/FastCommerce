using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class RemovedStockPropertyCombinationIdFromPropertyDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails");

            migrationBuilder.AlterColumn<int>(
                name: "StockPropertyCombinationId",
                table: "PropertyDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails",
                column: "StockPropertyCombinationId",
                principalTable: "StockPropertyCombinations",
                principalColumn: "StockPropertyCombinationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails");

            migrationBuilder.AlterColumn<int>(
                name: "StockPropertyCombinationId",
                table: "PropertyDetails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails",
                column: "StockPropertyCombinationId",
                principalTable: "StockPropertyCombinations",
                principalColumn: "StockPropertyCombinationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
