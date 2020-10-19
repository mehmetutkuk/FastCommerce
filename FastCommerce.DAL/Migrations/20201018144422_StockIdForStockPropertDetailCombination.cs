using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class StockIdForStockPropertDetailCombination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPropertyCombinations_Stocks_StockId",
                table: "StockPropertyCombinations");

            migrationBuilder.AlterColumn<int>(
                name: "StockId",
                table: "StockPropertyCombinations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPropertyCombinations_Stocks_StockId",
                table: "StockPropertyCombinations",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPropertyCombinations_Stocks_StockId",
                table: "StockPropertyCombinations");

            migrationBuilder.AlterColumn<int>(
                name: "StockId",
                table: "StockPropertyCombinations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_StockPropertyCombinations_Stocks_StockId",
                table: "StockPropertyCombinations",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
