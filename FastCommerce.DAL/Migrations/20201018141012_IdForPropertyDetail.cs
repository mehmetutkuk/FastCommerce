using Microsoft.EntityFrameworkCore.Migrations;

namespace FastCommerce.DAL.Migrations
{
    public partial class IdForPropertyDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetail_Properties_PropertyId",
                table: "PropertyDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_StockPropertyCombinations_PropertyDetail_PropertyDetailId",
                table: "StockPropertyCombinations");

            migrationBuilder.DropForeignKey(
                name: "FK_StockPropertyCombinations_Properties_PropertyID",
                table: "StockPropertyCombinations");

            migrationBuilder.DropIndex(
                name: "IX_StockPropertyCombinations_PropertyID",
                table: "StockPropertyCombinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyDetail",
                table: "PropertyDetail");

            migrationBuilder.DropColumn(
                name: "PropertyID",
                table: "StockPropertyCombinations");

            migrationBuilder.RenameTable(
                name: "PropertyDetail",
                newName: "PropertyDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetail_PropertyId",
                table: "PropertyDetails",
                newName: "IX_PropertyDetails_PropertyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyDetails",
                table: "PropertyDetails",
                column: "PropertyDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Properties_PropertyId",
                table: "PropertyDetails",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_PropertyDetails_Properties_PropertyId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StockPropertyCombinations_PropertyDetails_PropertyDetailId",
                table: "StockPropertyCombinations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyDetails",
                table: "PropertyDetails");

            migrationBuilder.RenameTable(
                name: "PropertyDetails",
                newName: "PropertyDetail");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyDetails_PropertyId",
                table: "PropertyDetail",
                newName: "IX_PropertyDetail_PropertyId");

            migrationBuilder.AddColumn<int>(
                name: "PropertyID",
                table: "StockPropertyCombinations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyDetail",
                table: "PropertyDetail",
                column: "PropertyDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_StockPropertyCombinations_PropertyID",
                table: "StockPropertyCombinations",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetail_Properties_PropertyId",
                table: "PropertyDetail",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPropertyCombinations_PropertyDetail_PropertyDetailId",
                table: "StockPropertyCombinations",
                column: "PropertyDetailId",
                principalTable: "PropertyDetail",
                principalColumn: "PropertyDetailId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPropertyCombinations_Properties_PropertyID",
                table: "StockPropertyCombinations",
                column: "PropertyID",
                principalTable: "Properties",
                principalColumn: "PropertyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
