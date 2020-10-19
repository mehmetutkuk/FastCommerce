using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FastCommerce.DAL.Migrations
{
    public partial class StockPropertyCombinationIdAdedToStockPropertyCombination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockPropertyCombinations",
                table: "StockPropertyCombinations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StockPropertyCombinations");

            migrationBuilder.AddColumn<int>(
                name: "StockPropertyCombinationId",
                table: "StockPropertyCombinations",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockPropertyCombinations",
                table: "StockPropertyCombinations",
                column: "StockPropertyCombinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails",
                column: "StockPropertyCombinationId",
                principalTable: "StockPropertyCombinations",
                principalColumn: "StockPropertyCombinationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StockPropertyCombinations",
                table: "StockPropertyCombinations");

            migrationBuilder.DropColumn(
                name: "StockPropertyCombinationId",
                table: "StockPropertyCombinations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StockPropertyCombinations",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockPropertyCombinations",
                table: "StockPropertyCombinations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_StockPropertyCombinations_StockPropertyComb~",
                table: "PropertyDetails",
                column: "StockPropertyCombinationId",
                principalTable: "StockPropertyCombinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
